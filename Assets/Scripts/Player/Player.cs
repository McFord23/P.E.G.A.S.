using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float speed;
    public float angle;
    public float angleOfAttack;
    public bool landed;

    bool haveHeart = false;

    public Vector2 spawnPos;

    Vector2 saveDirection;
    MoveState saveState;

    public Rigidbody2D rb { get; private set; }
    PolygonCollider2D flyCollider;
    CapsuleCollider2D deathCollider;
    CircleCollider2D[] rideColliders = new CircleCollider2D[2];

    public MoveState moveState;
    
    private Animator animatorController;
    private SoundController soundController;
    private PlayersManager playersManager;

    private Heart heart;

    // Dev ops
    public bool godnessMode = false;

    // Fly Physics
    public float wingSpan = 13.56f;
    public float wingArea = 78.04f;
    private float aspectRatio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = Mathf.Epsilon;
        aspectRatio = (wingSpan * wingSpan) / wingArea;
        flyCollider = GetComponentInChildren<PolygonCollider2D>();
        deathCollider = GetComponentInChildren<CapsuleCollider2D>();
        rideColliders = GetComponentsInChildren<CircleCollider2D>();

        soundController = SoundController.Instance;
        animatorController = GetComponentInChildren<Animator>();
        playersManager = PlayersManager.Instance;
        playersManager.LoadPlayer(this);
        spawnPos = playersManager.transform.position;
        
        heart = playersManager.heart;
        Idle();
    }

    void FixedUpdate()
    {
        if (moveState is MoveState.Idle or MoveState.Run)
        {
            RidePhysics();
        }

        if (moveState is MoveState.Flap or MoveState.FreeFall)
        {
            FlyPhysics();
        }

        if (moveState == MoveState.Dead)
        {
            rb.drag = landed ? 3f : 1f;
        }

        if (rb.velocity.x > 0.01) transform.localScale = new Vector3(1, 1, 1);
        else if (rb.velocity.x < -0.01) transform.localScale = new Vector3(1, -1, 1);

        speed = rb.velocity.magnitude;
        angle = transform.eulerAngles.z;
    }

    public void Reset()
    {
        var localTransform = transform;
        localTransform.localScale = new Vector3(1, 1, 1);
        rb.velocity = new Vector2(0, 0);
        rb.drag = Mathf.Epsilon;
        rb.angularVelocity = 0f;
        localTransform.right = Vector2.right;
        localTransform.position = spawnPos;
        Resurrect();
        haveHeart = false;
    }

    private void Resurrect()
    {
        deathCollider.enabled = false;
        foreach (var rideCollider in rideColliders)
        {
            rideCollider.enabled = true;
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
        if (landed) rb.AddForce(transform.right * (gear * torque));
    }

    void RidePhysics()
    {
        var friction = (landed) ? -0.3f : 0f;
        if (moveState == MoveState.Idle) friction = -rb.mass;

        var velocity = rb.velocity;
        var drag = 0.021f * velocity.sqrMagnitude;
        var dragDirection = -velocity.normalized;

        rb.AddForce(dragDirection * drag + velocity * friction);
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
        rb.AddForce(transform.right * (gear * flapForce));
    }

    void FlyPhysics()
    {
        var velocity = rb.velocity;
        var localVelocity = transform.InverseTransformDirection(velocity);
        angleOfAttack = Mathf.Atan2(localVelocity.y, localVelocity.x);

        var inducedLift = angleOfAttack * (aspectRatio / (aspectRatio + 2f)) * 2f * Mathf.PI;
        var inducedDrag = (inducedLift * inducedLift) / (aspectRatio * Mathf.PI);
        var pressure = velocity.sqrMagnitude * 1.2754f * 0.5f * wingArea;
        var lift = inducedLift * pressure;
        var drag = (0.021f + inducedDrag) * pressure;

        var dragDirection = -(Vector3)velocity.normalized;
        var liftDirection = Vector3.Cross(dragDirection, -transform.forward);

        // Lift + Drag = Total Force
        rb.AddForce(liftDirection * lift + dragDirection * drag);
    }

    public void Kill()
    {
        if (godnessMode) return;
        moveState = MoveState.Dead;
        deathCollider.enabled = true;
        flyCollider.enabled = false;
        animatorController.Play("Dead");
        rb.gravityScale = 1f;
            
        playersManager.Dead();

        if (haveHeart) SetHeart(false);
        heart.DropTarget();
    }

    private void SetHeart(bool var)
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
        if (moveState is MoveState.Idle or MoveState.Run) return;
        if (collision.gameObject.CompareTag("Player")) return;
        if (!godnessMode && (moveState != MoveState.Dead))
        {
            Kill();
        }
                
        soundController.Hit();
    }

    void OnCollisionExit2D()
    {
        landed = false;

        if (moveState is not (MoveState.Idle or MoveState.Run)) return;
        
        foreach (var rideCollider in rideColliders)
        {
            rideCollider.enabled = false;
        }
        flyCollider.enabled = true;
        FreeFall();
    }
}

public enum MoveState
{
    Idle,
    Run,
    FreeFall,
    Flap,
    Dead,
    Paused,
    Winner
}