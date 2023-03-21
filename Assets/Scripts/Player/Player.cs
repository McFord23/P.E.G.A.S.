using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float angle;
    public float angleOfAttack;
    public bool landed;

    public Vector2 spawnPos;

    private Vector2 saveDirection;
    private MoveState saveState;

    public Rigidbody2D rb { private set; get; }
    private PolygonCollider2D flyCollider;
    private CapsuleCollider2D deathCollider;
    private CircleCollider2D[] rideColliders = new CircleCollider2D[2];

    public MoveState moveState;
    private Animator animatorController;
    public SoundController soundController;
    private PlayersController playersController;

    public enum MoveState
    {
        //Loaded,
        Idle,
        Run,
        FreeFall,
        Flap,
        Dead,
        Paused,
        Winner
    }

    private CollectingItem item;

    // Fly Physics
    public float wingSpan = 13.56f;
    public float wingArea = 78.04f;
    private float aspectRatio;

    private void Start()
    {
        spawnPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        rb.drag = Mathf.Epsilon;
        aspectRatio = (wingSpan * wingSpan) / wingArea;
        flyCollider = GetComponentInChildren<PolygonCollider2D>();
        deathCollider = GetComponentInChildren<CapsuleCollider2D>();
        rideColliders = GetComponentsInChildren<CircleCollider2D>();

        animatorController = GetComponentInChildren<Animator>();
        playersController = GetComponentInParent<PlayersController>();

        if (landed) Idle();
        else FreeFall();
    }

    private void FixedUpdate()
    {
        if (moveState == MoveState.Idle || moveState == MoveState.Run)
        {
            RidePhysics();
        }

        if (moveState == MoveState.Flap || moveState == MoveState.FreeFall)
        {
            FlyPhysics();
        }

        if (moveState == MoveState.Dead)
        {
            if (landed) rb.drag = 3f;
            else rb.drag = 0.3f;
        }

        if (rb.velocity.x > 0.0001f && transform.localScale.y == -1) transform.localScale = new Vector3(1, 1, 1);
        else if (rb.velocity.x < -0.0001f && transform.localScale.y == 1) transform.localScale = new Vector3(1, -1, 1);

        speed = rb.velocity.magnitude;
        angle = transform.eulerAngles.z;
    }

    public void Reset()
    {
        transform.localScale = new Vector3(1, 1, 1);
        rb.velocity = new Vector2(0, 0);
        rb.drag = Mathf.Epsilon;
        rb.angularDrag = 2.5f;
        rb.angularVelocity = 0f;
        transform.right = Vector2.right;
        transform.position = spawnPos;

        if (item != null)
        {
            item.Drop();
            item = null;
        }

        Resurrect();
    }

    public void Resurrect()
    {
        deathCollider.enabled = false;
        foreach (CircleCollider2D collider in rideColliders)
        {
            collider.enabled = true;
        }

        Idle();
    }

    public void Victory()
    {
        moveState = MoveState.Winner;
        animatorController.Play("FreeFall");

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(0, 0);
    }

    public void Pause()
    {
        saveState = moveState;
        saveDirection = rb.velocity;

        moveState = MoveState.Paused;
        animatorController.speed = 0;

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Resume()
    {
        moveState = saveState;
        animatorController.speed = 1;

        rb.gravityScale = 1f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(saveDirection * 500, ForceMode2D.Impulse);
    }

    public void Idle()
    {
        moveState = MoveState.Idle;
        animatorController.Play("Idle");
    }

    public void Run(float torque)
    {
        moveState = MoveState.Run;
        animatorController.Play("Walk");

        float gear;
        gear = (speed < 20) ? 1.5f : 1;
        if (landed) rb.AddForce(transform.right * gear * torque);
    }

    private void RidePhysics()
    {
        var friction = (landed) ? -0.3f : 0f;
        if (moveState == MoveState.Idle) friction = -rb.mass;
        
        var drag = 0.021f * rb.velocity.sqrMagnitude;
        var dragDirection = -rb.velocity.normalized;

        rb.AddForce(dragDirection * drag + rb.velocity * friction);
    }

    public void FreeFall()
    {
        moveState = MoveState.FreeFall;
        animatorController.Play("FreeFall");
    }

    public void Flap(float flapForce)
    {
        float gear;

        moveState = MoveState.Flap;
        animatorController.Play("Flap");

        gear = (speed < 20) ? 2 : 1;
        rb.AddForce(transform.right * gear * flapForce);
    }

    private void FlyPhysics()
    {
        var localVelocity = transform.InverseTransformDirection(rb.velocity);
        angleOfAttack = Mathf.Atan2(localVelocity.y, localVelocity.x);

        var inducedLift = angleOfAttack * (aspectRatio / (aspectRatio + 2f)) * 2f * Mathf.PI;
        var inducedDrag = (inducedLift * inducedLift) / (aspectRatio * Mathf.PI);
        var pressure = rb.velocity.sqrMagnitude * 1.2754f * 0.5f * wingArea;

        var lift = inducedLift * pressure;
        var drag = (0.021f + inducedDrag) * pressure;

        var dragDirection = -(Vector3)rb.velocity.normalized;
        var liftDirection = Vector3.Cross(dragDirection, -transform.forward);

        // Lift + Drag = Total Force
        rb.AddForce(liftDirection * lift + dragDirection * drag);
    }

    public void Dead()
    {
        moveState = MoveState.Dead;
        deathCollider.enabled = true;
        flyCollider.enabled = false;
        animatorController.Play("Dead");
        rb.gravityScale = 1f;
        rb.angularDrag = 0.3f;

        playersController.Dead();

        if (item != null)
        {
            item.Drop();
            item = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((moveState != MoveState.Idle) && (moveState != MoveState.Run))
        {
            soundController.Hit();
            if (moveState != MoveState.Dead) Dead();
        }
        else
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                landed = true;
            }
        }
    }

    private void OnCollisionExit2D()
    {
        landed = false;

        if (moveState == MoveState.Idle || moveState == MoveState.Run)
        {
            foreach (CircleCollider2D rideCollider in rideColliders)
            {
                rideCollider.enabled = false;
            }

            flyCollider.enabled = true;
            FreeFall();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Item") && item == null && moveState != MoveState.Dead)
        {
            item = collider.gameObject.GetComponent<CollectingItem>();
            item.Pickup(transform);
        }
    }
}