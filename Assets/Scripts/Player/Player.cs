using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private const float SlowSpeed = 30f;
    private const float StartFlySpeed = 40f;
    private const float CrushSpeed = 70f;
    private const float StartFlyForce = 250f;
    private const float ReviveTime = 3f;

    [SerializeField] private GameObject crushEffectPrefab;
    
    public float speed;
    public float angle;
    public float angleOfAttack;
    public bool landed;

    public Vector2 spawnPos;

    private Vector2 saveDirection;
    private MoveState saveState;

    public Rigidbody2D rb { get; private set; }
    private PolygonCollider2D flyCollider;
    private CapsuleCollider2D deathCollider;
    private BoxCollider2D rideCollider;

    public MoveState moveState;
    
    private Animator animatorController;
    private SoundController soundController;
    private PlayersManager playersManager;

    private CollectingItem item;

    // Fly Physics
    public float wingSpan = 13.56f;
    public float wingArea = 78.04f;
    private float aspectRatio;

    private bool countReviveTimer;
    private float reviveTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = Mathf.Epsilon;
        aspectRatio = (wingSpan * wingSpan) / wingArea;
        flyCollider = GetComponentInChildren<PolygonCollider2D>();
        deathCollider = GetComponentInChildren<CapsuleCollider2D>();
        rideCollider = GetComponentInChildren<BoxCollider2D>();

        soundController = SoundController.Instance;
        animatorController = GetComponentInChildren<Animator>();
        playersManager = PlayersManager.Instance;
        playersManager.LoadPlayer(this);
        spawnPos = playersManager.transform.position;
        
        Idle();
    }

    private void Update()
    {
        UpdateReviveTime();
    }

    private void FixedUpdate()
    {
        if (moveState is MoveState.Idle or MoveState.Run)
        {
            RidePhysics();

            if (speed >= StartFlySpeed)
            {
                SetFlyMode(true);
            }
        }

        if (moveState is MoveState.Flap or MoveState.FreeFall)
        {
            FlyPhysics();
        }

        if (moveState == MoveState.Dead)
        {
            rb.drag = landed ? 3f : 0.3f;
        }

        HandleFlyFlip();

        speed = rb.velocity.magnitude;
        angle = transform.eulerAngles.z;
    }

    public void Revive(bool teleportBack = true)
    {
        var localTransform = transform;
        localTransform.localScale = new Vector3(1, 1, 1);
        rb.velocity = new Vector2(0, 0);
        rb.drag = Mathf.Epsilon;
        rb.angularDrag = 2.5f;
        rb.angularVelocity = 0f;
        localTransform.right = Vector2.right;

        if (teleportBack)
        {
            localTransform.position = spawnPos;
        }

        if ((bool)item)
        {
            item.Drop();
            item = null;
        }
        
        Resurrect();
    }

    private void UpdateReviveTime()
    {
        if (!countReviveTimer) return;
        
        reviveTimer -= Time.deltaTime;
        if (!(reviveTimer <= 0)) return;
        
        Revive(false);
        countReviveTimer = false;
    }

    private void Resurrect()
    {
        deathCollider.enabled = false;
        rideCollider.enabled = true;
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

        var gear = (speed < 20) ? 1.5f : 1;
        if (landed) rb.AddForce(transform.right * (gear * torque));
    }

    private void RidePhysics()
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
        moveState = MoveState.Flap;
        animatorController.Play("Flap");

        var gear = (speed < 20) ? 2 : 1;
        rb.AddForce(transform.right * (gear * flapForce));
    }

    private void FlyPhysics()
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

    private void HandleFlyFlip()
    {
        var localTransform = transform;
        var scaleY = rb.velocity.x switch
        {
            > 0.0001f => 1,
            < -0.0001f => -1,
            _ => localTransform.localScale.y
        };
        localTransform.localScale = new Vector3(1, scaleY, 1);
    }

    public void Kill()
    {
        landed = true;
        moveState = MoveState.Dead;
        deathCollider.enabled = true;
        flyCollider.enabled = false;
        animatorController.Play("Dead");
        rb.gravityScale = 1f;
        rb.angularDrag = 0.3f;
            
        if (speed >= CrushSpeed)
        {
            Instantiate(crushEffectPrefab, transform.position, Quaternion.identity);
            playersManager.Dead();
        }
        else
        {
            countReviveTimer = true;
            reviveTimer = ReviveTime;
        }

        if ((bool)item)
        {
            item.Drop();
            item = null;
        }
    }
    
    private void OnCollisionEnter2D()
    {
        if (moveState is MoveState.Idle or MoveState.Run or MoveState.Dead) return;

        if (speed <= SlowSpeed)
        {
            SetFlyMode(false);
            return;
        }
        
        soundController.Hit();
        Kill();
    }

   private void OnCollisionExit2D()
    {
        if (moveState is not (MoveState.Idle or MoveState.Run)) return;
        SetFlyMode(true);
    }
   
   private void SetFlyMode(bool fly)
   {
       rideCollider.enabled = !fly;
       flyCollider.enabled = fly;
        
       landed = !fly;
        
       if (fly)
       {
           FreeFall();
           rb.AddForce(Vector2.up * StartFlyForce);
       }
       else
       {
           rb.velocity = Vector3.zero;
           rb.angularVelocity = 0;
           Idle();
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

public enum MoveState
{
    Loaded,
    Idle,
    Run,
    FreeFall,
    Flap,
    Dead,
    Paused,
    Winner
}