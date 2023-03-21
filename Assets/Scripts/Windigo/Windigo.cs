using UnityEngine;

public class Windigo : MonoBehaviour
{
    public float speed;

    // Fly Physics
    public float wingSpan = 13.56f;
    public float wingArea = 78.04f;
    private float aspectRatio;

    private Vector2 saveDirection;
    private MoveState saveState;

    private Rigidbody2D rb;
    private MoveState moveState;

    private WindigosManager windigosManager;
    private SoundController soundController;

    public enum MoveState
    {
        FreeFall,
        Flap,
        Dead,
        Paused,
        Winner
    }

    private CollectingItem item;

    public MoveState GetMoveState()
    {
        return moveState;
    }

    public void Start()
    {
        windigosManager = transform.parent.GetComponent<WindigosManager>();
        soundController = windigosManager.soundController;

        rb = GetComponent<Rigidbody2D>();
        rb.drag = Mathf.Epsilon;
        aspectRatio = (wingSpan * wingSpan) / wingArea;
        
        FreeFall();
    }

    private void FixedUpdate()
    {
        if (moveState == MoveState.Flap || moveState == MoveState.FreeFall)
        {
            FlyPhysics();
            Flap(speed);
        }
    }

    public void Reset()
    {
        moveState = MoveState.FreeFall;
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0f;
    }

    public void Pause()
    {
        saveState = moveState;
        saveDirection = rb.velocity;

        moveState = MoveState.Paused;

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Resume()
    {
        moveState = saveState;

        rb.gravityScale = 1f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(saveDirection * 100, ForceMode2D.Impulse);
    }

    public void FreeFall()
    {
        moveState = MoveState.FreeFall;
    }

    public void Flap(float flapForce)
    {
        float gear;

        moveState = MoveState.Flap;

        gear = (speed < 20) ? 2 : 1;
        rb.AddForce(transform.right * gear * flapForce);
    }

    private void FlyPhysics()
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

    private void Dead()
    {
        soundController.Hit();
        gameObject.layer = 8;
        moveState = MoveState.Dead;

        if (item != null)
        {
            item.Drop();
            item = null;
        }
    }

    public void Destroy()
    {
        if (moveState == MoveState.Dead)
        {
            windigosManager.Remove(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dead();
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