using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : MonoBehaviour
{
    bool active;
    public float power = 100f;
    public float speed;
    public float angle;
    public Vector3 direction;
    Rigidbody2D rb;

    public Player player;
    public SoundController soundController;

    public UnityEvent CannonShootEvent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        active = false;
        //soundController.cannonScratchSound.Play();
    }

    void Update()
    {
        if (active)
        {           
            if ((360 - transform.eulerAngles.z) > 300)
            {
                if (transform.eulerAngles.z < 55)
                {
                    rb.AddTorque(0.1f * Input.GetAxis("Mouse X"));
                    rb.AddTorque(0.1f * Input.GetAxis("Mouse Y"));
                    rb.AddTorque(Input.GetAxis("Rotate"));
                }
                else if ((Input.GetAxis("Mouse Y") < 0) || (Input.GetAxis("Rotate") < 0))
                {
                    rb.AddTorque(0.1f * Input.GetAxis("Mouse X"));
                    rb.AddTorque(0.1f * Input.GetAxis("Mouse Y"));
                    rb.AddTorque(Input.GetAxis("Rotate"));
                }
            }
            else
            {
                if ((Input.GetAxis("Mouse Y") > 0) || (Input.GetAxis("Rotate") > 0))
                {
                    rb.AddTorque(0.1f * Input.GetAxis("Mouse X"));
                    rb.AddTorque(0.1f * Input.GetAxis("Mouse Y"));
                    rb.AddTorque(Input.GetAxis("Rotate"));
                }
            }

            // gamepad axis conflicts with mouse / keyboard
            if ((360 - transform.eulerAngles.z) > 300)
            {
                if (transform.eulerAngles.z < 55)
                {
                    rb.AddTorque(Input.GetAxis("Rotate-Gamepad"));
                }
                else if (Input.GetAxis("Rotate-Gamepad") < 0)
                {
                    rb.AddTorque(Input.GetAxis("Rotate-Gamepad"));
                }
            }
            else
            {
                if (Input.GetAxis("Rotate-Gamepad") > 0)
                {
                    rb.AddTorque(Input.GetAxis("Rotate-Gamepad"));
                }
            }

            direction = transform.right;
            direction.Normalize();

            soundController.cannonScratchSound.volume = Mathf.Clamp(Mathf.Abs(rb.angularVelocity / 20), 0f, 0.5f);

            if (((Input.GetAxis("Horizontal") < 0) || (Input.GetAxis("Horizontal-Mouse") < 0)) && (power > 20000))
            {
                power -= 100f;
            }

            if (((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Horizontal-Mouse") > 0)) && (power < 40000))
            {
                power += 100f;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || (Input.GetAxis("Gas-Gamepad") > 0))
            {
                Shoot();
            }
        }

        angle = transform.eulerAngles.z;
    }

    public void Reset()
    {
        active = true;
        soundController.cannonScratchSound.Play();
    }

    public void Pause()
    {
        if (active)
        {
            active = false;
            soundController.cannonScratchSound.Pause();
        } 
    }

    public void Resume()
    {
        if (player.moveState == Player.MoveState.Loaded)
        {
            active = true;
            soundController.cannonScratchSound.UnPause();
        }
    }

    public void Shoot()
    {
        player.sprite.enabled = true;
        player.transform.right = transform.right;
        player.moveState = Player.MoveState.Flap;
        player.rb.AddForce(direction * power, ForceMode2D.Impulse);
        player.rb.gravityScale = 1f;
        active = false;

        CannonShootEvent.Invoke();
        soundController.cannonShootSound.Play();
        soundController.cannonScratchSound.Stop();   
    }
}
