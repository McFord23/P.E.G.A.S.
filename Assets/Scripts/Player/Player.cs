using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float maxRPM;
    public float minRPM;
    public float speed;
    public Vector2 velocity;

    public float angle;
    public float angleOfAttack;

    public Vector2 spawnPos;

    Vector2 saveDirection;
    MoveState saveState;

    public Renderer sprite;

    public PolygonCollider2D flyCollider;
    public CapsuleCollider2D deathCollider;
    public CircleCollider2D[] rideCollider;

    public Rigidbody2D rb;
    public Animator animatorController;
    public SoundController soundController;
    public MoveState moveState;

    public bool landed;

    public enum MoveState
    {
        Idle,
        Run,
        Loaded,
        FreeFall,
        Flap,
        FlapGamepad,
        Dead,
        Paused,
        Winner
    }

    public UnityEvent VictoryEvent;
    public UnityEvent PlayerDeadEvent;
    public UnityEvent PlayerResetEvent;
    public UnityEvent PlayerPausedEvent;
    public UnityEvent PlayerResumeEvent;
    public UnityEvent PlayerLoadInCannonEvent;

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
        rb = GetComponent<Rigidbody2D>();

        rb.drag = Mathf.Epsilon;
        aspectRatio = (wingSpan * wingSpan) / wingArea;

        spawnPos = transform.position;
        Idle();
        //LoadInCannon();
    }

    void FixedUpdate()
    {
        if (moveState == MoveState.Idle || moveState == MoveState.Run)
        {
            RidePhysics();
        }
        
        if ((moveState == MoveState.Idle || moveState == MoveState.Run) && (Input.GetAxis("Mouse Y") > 0) || Input.GetAxis("Rotate") > 0)
        {
            FlyPhysics();
            //transform.Rotate(0f, 0f, Save.MouseSensitivity * Input.GetAxis("Mouse X"));
            

            //rb.AddTorque(Input.GetAxis("Mouse Y") * rb.mass * speed / 44);
            //rb.AddTorque(Input.GetAxis("Rotate") * rb.mass * 5f * speed / 44);
        }

        if (moveState == MoveState.Flap || moveState == MoveState.FlapGamepad || moveState == MoveState.FreeFall)
        {
            FlyPhysics();
            //GravityRotation();
        }

        velocity = rb.velocity;
        speed = rb.velocity.magnitude;
        angle = transform.eulerAngles.z;
    }

    public void Reset()
    {
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0f;
        transform.right = Vector2.right;
        transform.position = spawnPos;

        deathCollider.enabled = false;
        foreach (CircleCollider2D collider in rideCollider)
        {
            collider.enabled = true;
        }

        //LoadInCannon(); // for working DevOps-teleportation
        Idle();

        PlayerResetEvent.Invoke();
    }

    public void LoadInCannon()
    {
        moveState = MoveState.Loaded;
        rb.gravityScale = 0f;
        sprite.enabled = false;
        PlayerLoadInCannonEvent.Invoke();
    }

    public void Victory()
    {
        moveState = MoveState.Winner;
        animatorController.Play("FreeFall");

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(0, 0);

        VictoryEvent.Invoke();
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

        PlayerPausedEvent.Invoke();
    }

    public void Resume()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        moveState = saveState;
        animatorController.speed = 1;
        if (moveState != MoveState.Loaded) rb.gravityScale = 1f;
        rb.AddForce(saveDirection * 500, ForceMode2D.Impulse);

        PlayerResumeEvent.Invoke();
    }

    public void Idle()
    {
        moveState = MoveState.Idle;
        animatorController.Play("Idle");
    }

    public void Walk(float torque)
    {
        moveState = MoveState.Run;
        animatorController.Play("Walk");

        float gear;
        gear = (speed < 20) ? 1.5f : 1;

        if (landed) rb.AddForce(transform.right * gear * torque);
    }

    void RidePhysics()
    {
        var friction = (landed) ? -1f : 0f;
        if (moveState == MoveState.Idle) friction *= rb.mass;
        
        var drag = 0.021f * rb.velocity.sqrMagnitude;
        var dragDirection = -(Vector2)rb.velocity.normalized;

        rb.AddForce(dragDirection * drag + friction * rb.velocity);
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
        //soundController.Flap();
    }

    public void Flap(float flapForce, string device)
    {
        float gear;
        
        moveState = (device == "Gamepad") ? MoveState.FlapGamepad : MoveState.Flap;
        animatorController.Play("Flap");

        gear = (speed < 20) ? 2 : 1;
        rb.AddForce(Vector3.right * gear * flapForce);
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

    void Dead()
    {
        if (!godnessMode)
        {
            moveState = MoveState.Dead;
            deathCollider.enabled = true;
            flyCollider.enabled = false;
            animatorController.Play("Dead");
            rb.gravityScale = 1f;
            PlayerDeadEvent.Invoke();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            landed = true;
        }
    }

    void OnCollisionEnter2D()
    {
        if ((moveState != MoveState.Idle) && (moveState != MoveState.Run))
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