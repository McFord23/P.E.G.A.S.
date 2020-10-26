using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float flapForce = 7f;
    float flapTime = 0;
    float flapCooldown = 0.375f;
    public float speed = 0;
    public float acceleration;

    public Vector2 spawnPos;

    Vector2 mousePos;
    Vector2 pegasPos;
    Vector2 flapDirection;

    Vector2 saveDirection;
    MoveState saveState;

    public Renderer sprite;

    public PolygonCollider2D liveCollider;
    public CapsuleCollider2D deathCollider;

    public Rigidbody2D rb;
    public Animator animatorController;
    public SoundController soundController;
    public MoveState moveState = MoveState.FreeFall;

    public enum MoveState
    {
        Loaded,
        FreeFall,
        Hover,
        Flap,
        Dead,
        Paused
    }

    public UnityEvent VictoryEvent;
    public UnityEvent PlayerDeadEvent;
    public UnityEvent PlayerResetEvent;
    public UnityEvent PlayerPausedEvent;
    public UnityEvent PlayerResumeEvent;

    // Dev ops
    public bool godnessMode = false;
    public bool deathIndicator = false;
    public bool pullClick = false;

    ////////////////////////////////////////////

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spawnPos = transform.position;
        LoadInCannon();
    }

    void FixedUpdate()
    {        
        if (moveState == MoveState.Flap)
        {
            flapTime -= Time.deltaTime;
            if (flapTime <= 0)
            {
                FreeFall();
            }
        }

        if (moveState == MoveState.Flap || moveState == MoveState.Hover || moveState == MoveState.FreeFall)
        {
            //CalculateDirection();
            FlyPhysics();
        }

        if (moveState == MoveState.FreeFall)
        {
            rb.AddForce(transform.right * rb.velocity.magnitude, ForceMode2D.Force);
        }

        acceleration = (rb.velocity.magnitude - speed) / Time.deltaTime;
        speed = rb.velocity.magnitude;
    }

    public void Reset()
    {
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0f;
        transform.right = Vector2.right;
        transform.position = spawnPos;
        liveCollider.enabled = true;
        deathCollider.enabled = false;
        LoadInCannon();

        PlayerResetEvent.Invoke();
    }

    void LoadInCannon()
    {
        moveState = MoveState.Loaded;
        rb.gravityScale = 0f;
        sprite.enabled = false;
    }

    public void Pause(string reason)
    {
        saveState = moveState;
        saveDirection = rb.velocity;

        moveState = MoveState.Paused;
        animatorController.Play("FreeFall");

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(0, 0);

        if (reason == "AFK") PlayerPausedEvent.Invoke();
        else if (reason == "Victory") VictoryEvent.Invoke();
    }

    public void Resume()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        moveState = saveState;
        if (moveState != MoveState.Loaded) rb.gravityScale = 1f;
        rb.AddForce(saveDirection, ForceMode2D.Impulse);

        PlayerResumeEvent.Invoke();
    }

    public void FreeFall()
    {
        moveState = MoveState.FreeFall;
        animatorController.Play("FreeFall");
    }

    public void Flap()
    {
        moveState = MoveState.Flap;
        flapTime = flapCooldown;
        animatorController.Play("Flap");
        rb.AddForce(transform.right * flapForce, ForceMode2D.Impulse);
        
        soundController.Flap();
    }

    public void Hover()
    {
        moveState = MoveState.Hover;
        animatorController.Play("Hover");
        rb.AddForce(flapDirection, ForceMode2D.Force);
        rb.gravityScale = 0f;
    }

    void CalculateDirection()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pegasPos = transform.position;

        flapDirection = mousePos - pegasPos;
        flapDirection.Normalize();

        if (moveState == MoveState.Hover)
        {
            transform.right = new Vector3(1, 0);
        }
        else
        {
            transform.right = flapDirection;
        }
    }

    void FlyPhysics()
    {
        transform.Rotate(0f, 0f, 15f * Input.GetAxis("Mouse Y"));
        //Vector2 localVelocity = transform.InverseTransformDirection(rb.velocity);

    }

    void Dead()
    {
        if (!godnessMode)
        {
            moveState = MoveState.Dead;
            deathCollider.enabled = true;
            liveCollider.enabled = false;
            animatorController.Play("Dead");
            PlayerDeadEvent.Invoke();
        }
    }

    void OnCollisionEnter2D()
    {
        if (!godnessMode && (moveState != Player.MoveState.Dead)) Dead();
        soundController.Hit();

        // DevOps.Pull()
        deathIndicator = true;
    }

    // DevOps.Pull()
    private void OnCollisionExit2D()
    {
        deathIndicator = false;
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