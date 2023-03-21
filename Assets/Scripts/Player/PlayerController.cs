using Enums;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
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
        if (player.moveState is MoveState.Idle or MoveState.Run)
        {
            if (gasInput > 0)
            {
                player.Run((gasInput) * wingPower);
            }

            transform.Rotate(0f, 0f, rotateInput * player.speed / player.rb.mass);
        }

        if (player.moveState is MoveState.Flap or MoveState.FreeFall)
        {
            if (gasInput > 0)
            {
                player.Flap(gasInput * wingPower);
            }

            transform.Rotate(0f, 0f, rotateInput);
        }
    }

    void Update()
    {
        if (player.moveState == MoveState.Run)
        {
            if (gasInput == 0)
            {
                player.Idle();
            }
        }

        if (player.moveState == MoveState.Flap)
        {
            if (gasInput == 0)
            {
                player.FreeFall();
            }
        }

        if (IsOwner || Save.gameMode == GameMode.Single)
        {
            UpdatePlayerInput();
        }
    }
    
    private void UpdatePlayerInput()
    {
        var gas = 0f;
        var rotate = 0f;
        var shoot = 0f;
        
        var input = Save.players[0].controlLayout;
        
        switch (input)
        {
            case ControlLayout.Mouse:
                gas = Input.GetKey(KeyCode.Mouse0) ? 1f : 0f;
                rotate = Save.Sensitivity.mouse * Input.GetAxis("Rotate-Mouse");
                shoot = Input.GetKey(KeyCode.Mouse1) ? 1f : 0f;
                break;
           
            case ControlLayout.Wasd:
                gas = Input.GetKey(KeyCode.LeftShift) ? 1f : 0f;
                rotate = Save.Sensitivity.keyboard * Input.GetAxis("Rotate-WASD");
                shoot = Input.GetKey(KeyCode.LeftControl) ? 1f : 0f;
                break;
            
            case ControlLayout.Ijkl:
                gas = Input.GetKey(KeyCode.RightShift) ? 1f : 0f;
                rotate = Save.Sensitivity.keyboard * Input.GetAxis("Rotate-IJKL");
                shoot = Input.GetKey(KeyCode.RightControl) ? 1f : 0f;
                break;
           
            case ControlLayout.Arrow:
                gas = Input.GetKey(KeyCode.RightShift) ? 1f : 0f;
                rotate = Save.Sensitivity.keyboard * Input.GetAxis("Rotate-Arrow");
                shoot = Input.GetKey(KeyCode.RightControl) ? 1f : 0f;
                break;
           
            case ControlLayout.Numpad:
                gas = Input.GetKey(KeyCode.Space) ? 1f : 0f;
                rotate = Save.Sensitivity.keyboard * Input.GetAxis("Rotate-Numpad");
                shoot = Input.GetKey(KeyCode.CapsLock) ? 1f : 0f;
                break;
        }

        int gamepad = Save.players[0].gamepad;
        bool gamepadActive = (gamepad == 1) ? Gamepad.gamepad1 : Gamepad.gamepad2;
        if (gamepadActive)
        {
            gas = Mathf.Clamp01(gas + Input.GetAxis("Gas-Gamepad " + gamepad));
            rotate += Save.Sensitivity.gamepad * Input.GetAxis("Rotate-Gamepad " + gamepad);
            shoot = Mathf.Clamp01(shoot + Input.GetAxis("Shoot-Gamepad " + gamepad));
        }

        SetInput(gas, rotate, shoot);
    }

    private void SetInput(float g, float r, float s)
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
