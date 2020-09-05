using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject deadMenu;
    public new CameraFollow camera;

    public Rigidbody2D rb;
    Animator animatorController;
    public MoveState moveState = MoveState.FreeFall;

    // Flap properties
    float flapForce = 8f;
    float flapTime = 0;
    float flapCooldown = 0.375f;
    public float speed = 0;
    public float acceleration;
    

    // Direction propeties
    Vector2 spawnPos;
    
    Vector2 mousePos;
    Vector2 pegasPos;
    Vector2 flapDirection;

    Vector2 saveDirection;
    MoveState saveState;

    // Dev ops
    public bool godnessMode = false;
    public bool deathIndicator = false;
    public bool pullClick = false;

    public enum MoveState
    {
        Start,
        FreeFall,
        Hover,
        Flap,
        SlowFall,
        SlowHover,
        SlowFlap,
        Dead,
        Paused
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorController = GetComponentInChildren<Animator>();

        spawnPos = transform.position;
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
            camera.FocusOnFly();
            CalculateDirection();
        }

        acceleration = (rb.velocity.magnitude - speed) / Time.deltaTime;
        
        if (moveState == MoveState.FreeFall)
        {
            rb.AddForce(flapDirection * rb.velocity.magnitude, ForceMode2D.Force);
        }

        speed = rb.velocity.magnitude;
    }

    public void Reset()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = spawnPos;
        FreeFall();

        deadMenu.SetActive(false);
        camera.FocusOnFly();
    }

    public void Paused()
    {
        saveState = moveState;
        saveDirection = rb.velocity;

        moveState = MoveState.Paused;
        animatorController.Play("FreeFall");

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(0, 0);
    }

    public void Resumed()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 1f;
        moveState = saveState;
        rb.AddForce(saveDirection, ForceMode2D.Impulse);
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
        rb.AddForce(flapDirection * flapForce, ForceMode2D.Impulse);
    }

    public void Hover()
    {
        moveState = MoveState.Hover;
        animatorController.Play("Flap");
        rb.velocity = flapDirection * flapForce;
    }

    void CalculateDirection()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pegasPos = transform.position;

        flapDirection = mousePos - pegasPos;
        flapDirection.Normalize();

        transform.right = flapDirection;
    }

    void Dead()
    {
        if (!godnessMode)
        {
            moveState = MoveState.Dead;
            animatorController.Play("Dead");

            deadMenu.SetActive(true);
            camera.FocusOnPlayer();
        }
    }

    void OnCollisionEnter2D()
    {
        //Dead();

        // Dev Ops
        if (!godnessMode) Dead();
        deathIndicator = true;
    }

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