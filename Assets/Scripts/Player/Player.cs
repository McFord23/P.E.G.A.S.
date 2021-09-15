using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float angle;
    public float angleOfAttack;
    public bool landed;

    bool haveHeart = false;

    public Vector2 spawnPos;

    Vector2 saveDirection;
    MoveState saveState;

    public Rigidbody2D rb;
    PolygonCollider2D flyCollider;
    CapsuleCollider2D deathCollider;
    CircleCollider2D[] rideCollider = new CircleCollider2D[2];

    public MoveState moveState;
    Animator animatorController;
    public SoundController soundController;
    PlayersController playersController;

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

    Heart heart;

    // Dev ops
    public bool godnessMode = false;
    public bool deathIndicator = false;
    public bool pullClick = false;

    // Fly Physics
    public float wingSpan = 13.56f;
    public float wingArea = 78.04f;
    float aspectRatio;

    void Start()
    {
        spawnPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        rb.drag = Mathf.Epsilon;
        aspectRatio = (wingSpan * wingSpan) / wingArea;
        flyCollider = GetComponentInChildren<PolygonCollider2D>();
        deathCollider = GetComponentInChildren<CapsuleCollider2D>();
        rideCollider = GetComponentsInChildren<CircleCollider2D>();

        animatorController = GetComponentInChildren<Animator>();
        playersController = GetComponentInParent<PlayersController>();

        if (landed) Idle();
        else FreeFall();

        heart = playersController.heart;
    }

    void FixedUpdate()
    {
        if (moveState == MoveState.Idle || moveState == MoveState.Run)
        {
            RidePhysics();
        }

        if (moveState == MoveState.Flap || moveState == MoveState.FreeFall)
        {
            FlyPhysics();
            //GravityRotation();
        }

        if (moveState == MoveState.Dead)
        {
            if (landed) rb.drag = 3f;
            else rb.drag = 1f;
        }

        if (rb.velocity.x > 0.01) transform.localScale = new Vector3(1, 1, 1);
        else if (rb.velocity.x < -0.01) transform.localScale = new Vector3(1, -1, 1);

        speed = rb.velocity.magnitude;
        angle = transform.eulerAngles.z;
    }

    public void Reset()
    {
        transform.localScale = new Vector3(1, 1, 1);
        rb.velocity = new Vector2(0, 0);
        rb.drag = Mathf.Epsilon;
        rb.angularVelocity = 0f;
        transform.right = Vector2.right;
        transform.position = spawnPos;
        Resurrect();
        haveHeart = false;
    }

    public void Resurrect()
    {
        deathCollider.enabled = false;
        foreach (CircleCollider2D collider in rideCollider)
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
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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

    void RidePhysics()
    {
        var friction = (landed) ? -0.3f : 0f;
        if (moveState == MoveState.Idle) friction = -rb.mass;
        
        var drag = 0.021f * rb.velocity.sqrMagnitude;
        var dragDirection = -(Vector2)rb.velocity.normalized;

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

    void FlyPhysics()
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

    void GravityRotation()
    {
        var gravityRotation = 1 / (1 + speed);
        if (((transform.eulerAngles.z > 300f) && (transform.eulerAngles.z < 360f)) || ((transform.eulerAngles.z > 0f) && (transform.eulerAngles.z < 90f)))
        {
            transform.Rotate(0f, 0f, -2.5f * gravityRotation);
            //rb.AddTorque(-500f * gravityRotation);
        }
        else if ((transform.eulerAngles.z > 90f) && (transform.eulerAngles.z < 240f))
        {
            transform.Rotate(0f, 0f, 2.5f * gravityRotation);
            //rb.AddTorque(500f * gravityRotation);
        }
        /*else
        {
            rb.angularVelocity = 0;
        }*/
    }

    public void Dead()
    {
        if (!godnessMode)
        {
            moveState = MoveState.Dead;
            deathCollider.enabled = true;
            flyCollider.enabled = false;
            animatorController.Play("Dead");
            rb.gravityScale = 1f;
            
            playersController.Dead();

            if (haveHeart) SetHeart(false);
            heart.DropTarget();
        }
    }

    public void SetHeart(bool var)
    {
        haveHeart = var;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            landed = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((moveState != MoveState.Idle) && (moveState != MoveState.Run))
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                if (!godnessMode && (moveState != MoveState.Dead))
                {
                    Dead();
                }
                soundController.Hit();

                // DevOps.Pull()
                deathIndicator = true;
            }
        }
    }

    void OnCollisionExit2D()
    {
        // DevOps.Pull()
        deathIndicator = false;
        landed = false;

        if (moveState == MoveState.Idle || moveState == MoveState.Run)
        {
            foreach (CircleCollider2D collider in rideCollider)
            {
                collider.enabled = false;
            }
            flyCollider.enabled = true;
            FreeFall();
        }
    }

    void OnMouseDown()
    {
        pullClick = true;
    }

    void OnMouseUp()
    {
        pullClick = false;
    }
}