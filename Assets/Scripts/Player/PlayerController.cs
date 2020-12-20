using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Player player;
    Castling character;

    void Start()
    {
        player = GetComponent<Player>();
        character = GetComponent<Castling>();
    }

    void FixedUpdate()
    {
        if (player.moveState == Player.MoveState.Idle || player.moveState == Player.MoveState.Run)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                player.Walk(2500);
            }

            transform.Rotate(0f, 0f, Input.GetAxis("Mouse Y") * player.speed / player.rb.mass);
            transform.Rotate(0f, 0f, Input.GetAxis("Rotate") * player.speed / player.rb.mass);
        }

        if (player.moveState == Player.MoveState.Flap || player.moveState == Player.MoveState.FlapGamepad || player.moveState == Player.MoveState.FreeFall)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                player.Flap(10000);
            }

            if (Input.GetAxis("Gas-Gamepad") > 0)
            {
                player.Flap(Input.GetAxis("Gas-Gamepad") * 10000, "Gamepad");
            }
            
            //player.rb.AddTorque(player.rb.mass * Save.MouseSensitivity * Input.GetAxis("Mouse Y"));
            //player.rb.AddTorque(player.rb.mass * Input.GetAxis("Rotate"));

            transform.Rotate(0f, 0f, Save.MouseSensitivity * Input.GetAxis("Mouse X"));
            transform.Rotate(0f, 0f, Save.MouseSensitivity * Input.GetAxis("Mouse Y"));
            transform.Rotate(0f, 0f, Save.KeyboardSensitivity * Input.GetAxis("Rotate"));

            transform.Rotate(0f, 0f, 6 * Input.GetAxis("Rotate-Gamepad"));
        }
    }

    void Update()
    {
        /*if (player.moveState == Player.MoveState.Idle || player.moveState == Player.MoveState.Run)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                player.Walk(Input.GetAxis("Horizontal") * 2500);
            }
            else
            {
                player.Idle();
            }
        }*/

        if (player.moveState == Player.MoveState.Run)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                player.Idle();
            }
        }

        if (player.moveState == Player.MoveState.Flap)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                player.FreeFall();
            }
        }

        if (player.moveState == Player.MoveState.FlapGamepad)
        {
            if (Input.GetAxis("Gas-Gamepad") == 0)
            {
                player.FreeFall();
            }
        }

        if (player.moveState != Player.MoveState.Paused && player.moveState != Player.MoveState.Dead && player.moveState != Player.MoveState.Winner)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                player.Pause();
            }
        }
    }
}
