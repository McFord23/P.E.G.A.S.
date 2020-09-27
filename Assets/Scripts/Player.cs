using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Cannon cannon;
    public CameraFollow cam;
    public Renderer sprite;

    GameObject pauseMenu;
    GameObject deadMenu;
    
    public AudioSource clickSound;

    public Rigidbody2D rb;
    public Animator animatorController;
    public MoveState moveState = MoveState.FreeFall;

    public float flapForce = 7f;
    float flapTime = 0;
    float flapCooldown = 0.4f; // 0.375f;
    public float speed = 0;
    public float acceleration;

    public Vector2 spawnPos;
    
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
        Loaded,
        FreeFall,
        Hover,
        Flap,
        Dead,
        Paused
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pauseMenu = GetComponent<PlayerKeyboardController>().pauseMenu;
        deadMenu = GetComponent<PlayerKeyboardController>().deadMenu;

        spawnPos = transform.position;
        Reset();
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
        sprite.enabled = false;
        rb.gravityScale = 0f;
        moveState = MoveState.Loaded;
        transform.position = spawnPos;
        sprite.enabled = false;

        deadMenu.SetActive(false);
        cam.FocusOnCannon();
        cannon.active = true;
    }

    public void Pause()
    {
        saveState = moveState;
        saveDirection = rb.velocity;

        moveState = MoveState.Paused;
        animatorController.Play("FreeFall");

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(0, 0);

        cannon.active = false;
        cam.FocusOnPlayer();
    }

    public void Resume()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        moveState = saveState;
        if (moveState != MoveState.Loaded) rb.gravityScale = 1f;
        rb.AddForce(saveDirection, ForceMode2D.Impulse);

        cannon.active = true;
        cam.FocusOnFly();
    }

    public void Shoot()
    {
        sprite.enabled = true;
        FreeFall();
        rb.AddForce(cannon.direction * cannon.power, ForceMode2D.Impulse);
        rb.gravityScale = 1f;

        cannon.Shoot();
        cam.FocusOnFly();
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

    void Dead()
    {
        if (!godnessMode)
        {
            moveState = MoveState.Dead;
            animatorController.Play("Dead");

            if (pauseMenu.activeSelf) pauseMenu.SetActive(false);
            clickSound.Play();
            deadMenu.SetActive(true);
            cam.FocusOnPlayer();
        }
    }

    void OnCollisionEnter2D()
    {
        if (!godnessMode) Dead();

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