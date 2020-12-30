using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayersController : MonoBehaviour
{
    GameObject celestia;
    GameObject luna;

    Player player1;
    PlayerController player1Controller;

    Player player2;
    PlayerController player2Controller;

    float gas = 0f;
    float rotate = 0f;
    float shoot = 0f;

    public UnityEvent PauseEvent;
    public UnityEvent ResumeEvent;
    public UnityEvent DeadEvent;
    public UnityEvent ResetEvent;
    public UnityEvent VictoryEvent;


    void Start()
    {
        celestia = transform.Find("Celestia").gameObject;
        luna = transform.Find("Luna").gameObject;

        if (Save.Player1.character == "Celestia")
        {
            player1 = celestia.GetComponent<Player>();
            player1Controller = celestia.GetComponent<PlayerController>();

            player2 = luna.GetComponent<Player>();
            player2Controller = luna.GetComponent<PlayerController>();
        }
        else
        {
            player1 = luna.GetComponent<Player>();
            player1Controller = luna.GetComponent<PlayerController>();

            player2 = celestia.GetComponent<Player>();
            player2Controller = celestia.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (Save.TogetherMode)
        {
            UpdateLayout(player1Controller);
            UpdateLayout(player2Controller);

            if (player1.moveState != Player.MoveState.Paused && player1.moveState != Player.MoveState.Winner)
            {
                if (player2.moveState != Player.MoveState.Paused && player2.moveState != Player.MoveState.Winner)
                {
                    if (player1.moveState != Player.MoveState.Dead && player2.moveState != Player.MoveState.Dead)
                    {
                        if (Input.GetButtonDown("Cancel"))
                        {
                            Pause();
                        }
                    }
                }
            }
        }
        else
        {
            UpdateLayout(player1Controller);
            OffInput(player2Controller);

            if (player1.moveState != Player.MoveState.Paused && player1.moveState != Player.MoveState.Dead && player1.moveState != Player.MoveState.Winner)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    Pause();
                }
            }
        }  
    }

    public void ChangeCharacter()
    {
        if (Save.Player1.character == "Celestia")
        {
            Save.Player1.character = "Luna";
            Save.Player2.character = "Celestia";

            player1 = luna.GetComponent<Player>();
            player1Controller = luna.GetComponent<PlayerController>();

            player2 = celestia.GetComponent<Player>();
            player2Controller = celestia.GetComponent<PlayerController>();
        }
        else
        {
            Save.Player1.character = "Celestia";
            Save.Player2.character = "Luna";

            player1 = celestia.GetComponent<Player>();
            player1Controller = celestia.GetComponent<PlayerController>();

            player2 = luna.GetComponent<Player>();
            player2Controller = luna.GetComponent<PlayerController>();
        }
    }

    public void UpdateLayout(PlayerController player)
    {
        string layout = (player == player1Controller) ? Save.Player1.controlLayout : Save.Player2.controlLayout;
        switch (layout)
        {
            case "mouse":
                gas = Input.GetKey(KeyCode.Mouse0) ? 1f : 0f;
                rotate = Save.Sensitivity.mouse * Input.GetAxis("Rotate-Mouse");
                shoot = Input.GetKey(KeyCode.Mouse1) ? 1f : 0f;
                break;
           
            case "wasd":
                gas = Input.GetKey(KeyCode.LeftShift) ? 1f : 0f;
                rotate = Save.Sensitivity.keyboard * Input.GetAxis("Rotate-WASD");
                shoot = Input.GetKey(KeyCode.LeftControl) ? 1f : 0f;
                break;
            
            case "ijkl":
                gas = Input.GetKey(KeyCode.RightShift) ? 1f : 0f;
                rotate = Save.Sensitivity.keyboard * Input.GetAxis("Rotate-IJKL");
                shoot = Input.GetKey(KeyCode.RightControl) ? 1f : 0f;
                break;
           
            case "arrow":
                gas = Input.GetKey(KeyCode.RightShift) ? 1f : 0f;
                rotate = Save.Sensitivity.keyboard * Input.GetAxis("Rotate-Arrow");
                shoot = Input.GetKey(KeyCode.RightControl) ? 1f : 0f;
                break;
           
            case "numpad":
                gas = Input.GetKey(KeyCode.Space) ? 1f : 0f;
                rotate = Save.Sensitivity.keyboard * Input.GetAxis("Rotate-Numpad");
                shoot = Input.GetKey(KeyCode.CapsLock) ? 1f : 0f;
                break;
        }

        int gamepad = (player == player1Controller) ? Save.Player1.gamepad : Save.Player2.gamepad;
        bool gamepadActive = (gamepad == 1) ? Gamepad.gamepad1 : Gamepad.gamepad2;
        if (gamepadActive)
        {
            gas = Mathf.Clamp01(gas + Input.GetAxis("Gas-Gamepad " + gamepad));
            rotate += Save.Sensitivity.gamepad * Input.GetAxis("Rotate-Gamepad " + gamepad);
            shoot = Mathf.Clamp01(shoot + Input.GetAxis("Shoot-Gamepad " + gamepad));
        }

        player.SetInput(gas, rotate, shoot);
    }

    public void OffInput(PlayerController player)
    {
        gas = 0f;
        rotate = 0f;
        shoot = 0f;
        player.SetInput(gas, rotate, shoot);
    }

    // Gets
    public Vector3 GetPlayerPosition()
    {
        if (Save.TogetherMode)
        {
            var vector = new Vector3((player1.transform.position.x + player2.transform.position.x) / 2, (player1.transform.position.y + player2.transform.position.y) / 2, 0);
            return vector;
        }
        else
        {
            return player1.transform.position;
        }
    }

    public float GetDistanceBetweenPlayers()
    {
        var vector = player1.transform.position - player2.transform.position;
        return Mathf.Abs(vector.magnitude);
    }

    public float GetPlayerSpeed()
    {
        if(Save.TogetherMode)
        {
            return (player1.speed + player2.speed) / 2;
        }
        else
        {
            return player1.speed;
        }
    }


    // Working
    public void Pause()
    {
        player1.Pause();
        player2.Pause();

        PauseEvent.Invoke();
    }

    public void Resume()
    {
        player1.Resume();
        player2.Resume();

        ResumeEvent.Invoke();
    }

    public void Dead()
    {
        if (Save.TogetherMode)
        {
            if (player1.moveState == Player.MoveState.Dead && player2.moveState == Player.MoveState.Dead)
            {
                DeadEvent.Invoke();
            }
        }
        else
        {
            DeadEvent.Invoke();
        }
    }

    public void Reset()
    {
        player1.Reset();
        player2.Reset();

        ResetEvent.Invoke();
    }

    public void Victory()
    {
        player1.Victory();
        player2.Victory();
        
        VictoryEvent.Invoke();
    }
}
