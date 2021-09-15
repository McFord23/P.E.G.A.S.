using UnityEngine;

public class Windigo : MonoBehaviour
{
    GameObject windigo;
    public float speed;
    float angle;

    // Fly Physics
    public float wingSpan = 13.56f;
    public float wingArea = 78.04f;
    float aspectRatio;

    Vector2 spawnPos;

    Vector2 saveDirection;
    MoveState saveState;

    Rigidbody2D rb;
    MoveState moveState;
    Animator animatorController;
    public SoundController soundController;

    public enum MoveState
    {
        FreeFall,
        Flap,
        Dead,
        Paused,
        Winner
    }

    public Heart heart;
    bool haveHeart = false;

    public MoveState GetMoveState()
    {
        return moveState;
    }

    void Start()
    {
        windigo = transform.gameObject;
        spawnPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        rb.drag = Mathf.Epsilon;
        aspectRatio = (wingSpan * wingSpan) / wingArea;
        animatorController = GetComponentInChildren<Animator>();
        
        FreeFall();
    }

    void FixedUpdate()
    {
        if (moveState == MoveState.Flap || moveState == MoveState.FreeFall)
        {
            FlyPhysics();
            Flap(speed);
        }

        angle = transform.eulerAngles.z;
    }

    public void Reset()
    {
        windigo.SetActive(true);
        moveState = MoveState.FreeFall;
        windigo.layer = 9;
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0f;
        transform.right = Vector2.right;
        transform.position = spawnPos;
    }

    public void Victory()
    {
        Dead();
        windigo.layer = 8;
        rb.AddForce(1000 * Vector3.right, ForceMode2D.Impulse);
    }

    public void Pause()
    {
        saveState = moveState;
        saveDirection = rb.velocity;

        moveState = MoveState.Paused;
        //animatorController.speed = 0;

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void Resume()
    {
        moveState = saveState;
        //animatorController.speed = 1;

        rb.gravityScale = 1f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(saveDirection * 100, ForceMode2D.Impulse);
    }

    public void FreeFall()
    {
        moveState = MoveState.FreeFall;
        //animatorController.Play("FreeFall");
    }

    public void Flap(float flapForce)
    {
        float gear;

        moveState = MoveState.Flap;
        //animatorController.Play("Flap");

        gear = (speed < 20) ? 2 : 1;
        rb.AddForce(transform.right * gear * flapForce);
        //soundController.Flap();
    }

    void FlyPhysics()
    {
        var localVelocity = transform.InverseTransformDirection(rb.velocity);
        var angleOfAttack = Mathf.Atan2(localVelocity.y, localVelocity.x);

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

    public void SetHeart(bool var)
    {
        haveHeart = var;
    }

    void Dead()
    {
        soundController.Hit();
        windigo.layer = 8;
        moveState = MoveState.Dead;

        if (haveHeart) SetHeart(false);
        heart.DropTarget();
    }

    public void Destroy()
    {
        if (moveState == MoveState.Dead)
        {
            windigo.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Dead();
    }
}