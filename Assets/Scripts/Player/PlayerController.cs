using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Player player;
    float wingPower = 10000;

    float gasInput = 0f;
    float rotateInput = 0f;
    float shootInput = 0f;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (player.moveState == Player.MoveState.Idle || player.moveState == Player.MoveState.Run)
        {
            if (gasInput > 0)
            {
                player.Run(gasInput * wingPower);
            }

            transform.Rotate(0f, 0f, rotateInput * player.speed / player.rb.mass);
        }

        if (player.moveState == Player.MoveState.Flap || player.moveState == Player.MoveState.FreeFall)
        {
            if (gasInput > 0)
            {
                player.Flap(gasInput * wingPower);
            }

            transform.Rotate(0f, 0f, rotateInput);
            //player.rb.AddTorque(player.rb.mass * rotate);
        }
    }

    void Update()
    {
        if (player.moveState == Player.MoveState.Run)
        {
            if (gasInput == 0)
            {
                player.Idle();
            }
        }

        if (player.moveState == Player.MoveState.Flap)
        {
            if (gasInput == 0)
            {
                player.FreeFall();
            }
        }

        if (player.moveState != Player.MoveState.Paused && player.moveState != Player.MoveState.Dead && player.moveState != Player.MoveState.Winner)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                player.Pause();
            }
        }
    }

    public void SetInput(float g, float r, float s)
    {
        gasInput = g;
        rotateInput = r;
        shootInput = s;
    }

    public float GetShootInput()
    {
        return shootInput;
    }
}
