using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayersController : MonoBehaviour
{
    private GameObject celestia;
    private GameObject luna;

    private Player player1;
    private PlayerController player1Controller;

    private Player player2;
    private PlayerController player2Controller;

    private Player survivor;

    private float gas = 0f;
    private float rotate = 0f;
    private float shoot = 0f;

    public UnityEvent PauseEvent;
    public UnityEvent ResumeEvent;
    public UnityEvent DeadEvent;
    public UnityEvent ResetEvent;
    public UnityEvent VictoryEvent;

    private void Start()
    {
        celestia = transform.Find("Celestia").gameObject;
        luna = transform.Find("Luna").gameObject;
        UpdateCharacter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (player1.moveState != Player.MoveState.Paused && player1.moveState != Player.MoveState.Winner)
            {
                if (!Save.TogetherMode)
                {
                    Reset();
                }
                else if (player2.moveState != Player.MoveState.Paused && player2.moveState != Player.MoveState.Winner)
                {
                    Reset();
                }
            }
        }

        if (Save.TogetherMode)
        {
            UpdateLayout(player1Controller);
            UpdateLayout(player2Controller);

            if (player1.moveState != Player.MoveState.Paused && player1.moveState != Player.MoveState.Winner)
            {
                if (player2.moveState != Player.MoveState.Paused && player2.moveState != Player.MoveState.Winner)
                {
                    if (!(player1.moveState == Player.MoveState.Dead && player2.moveState == Player.MoveState.Dead))
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

    public void UpdateCharacter()
    {
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

    public Vector3 GetPlayerPosition()
    {
        if (Save.TogetherMode)
        {
            if (player1.moveState == Player.MoveState.Dead && player2.moveState != Player.MoveState.Dead)
            {
                if (GetDistanceBetweenPlayers() > CameraController.maxSize) return player2.rb.position;
                else return new Vector3((player1.rb.position.x + player2.rb.position.x) / 2, (player1.rb.position.y + player2.rb.position.y) / 2, 0);
            }
            else if (player2.moveState == Player.MoveState.Dead && player1.moveState != Player.MoveState.Dead)
            {
                if (GetDistanceBetweenPlayers() > CameraController.maxSize) return player1.transform.position;
                else return new Vector3((player1.rb.position.x + player2.rb.position.x) / 2, (player1.rb.position.y + player2.rb.position.y) / 2, 0);
            }
            else if (player1.moveState == Player.MoveState.Dead && player2.moveState == Player.MoveState.Dead)
            {
                return survivor.rb.position;
            }
            else
            {
                return new Vector3((player1.rb.position.x + player2.rb.position.x) / 2, (player1.rb.position.y + player2.rb.position.y) / 2, 0);
            }
        }
        else
        {
            return player1.rb.position;
        }
    }

    public Vector3 GetPlayerPosition(int i)
    {
        switch (i)
        {
            case 1:
                return player1.rb.position;
            case 2:
                return player1.rb.position;
            default: throw new ArgumentException("Invalid player index: ");
        }
    }

    float GetDistanceBetweenPlayers()
    {
        var vector = player1.rb.position - player2.rb.position;
        return Mathf.Abs(vector.magnitude);
    }

    public float GetFocusSize()
    {
        if (player1.moveState == Player.MoveState.Dead || player2.moveState == Player.MoveState.Dead)
        {
            if (GetDistanceBetweenPlayers() > CameraController.maxSize) return CameraController.flySize;
            else return GetDistanceBetweenPlayers();
        }
        else return GetDistanceBetweenPlayers();
    }

    public float GetSpeed()
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

    public float GetDirection()
    {
        if (Save.TogetherMode)
        {
            if (player1.moveState == Player.MoveState.Dead)
            {
                return player2.transform.localScale.y;
            }
            else if (player2.moveState == Player.MoveState.Dead)
            {
                return player1.transform.localScale.y;
            }
            else return player1.transform.localScale.y;
        }
        else
        {
            return player1.transform.localScale.y;
        }
    }

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

    public void KillPlayer(int i)
    {
        switch (i)
        {
            case 1:
                player1.Dead();
                break;
            case 2:
                player2.Dead();
                break;
        }
    }

    public void Dead()
    {
        if (Save.TogetherMode)
        {
            if (player1.moveState == Player.MoveState.Dead && player2.moveState != Player.MoveState.Dead)
            {
                Save.Player1.live = false;
                survivor = player2;
            }
            else if (player1.moveState != Player.MoveState.Dead && player2.moveState == Player.MoveState.Dead)
            {
                Save.Player2.live = false;
                survivor = player1;
            }
            else DeadEvent.Invoke();
        }
        else
        {
            Save.Player1.live = false;
            DeadEvent.Invoke();
        }
    }

    public void Reset()
    {
        player1.Reset();
        Save.Player1.live = true;

        player2.Reset();
        Save.Player1.live = true;

        ResetEvent.Invoke();
    }

    public void Victory()
    {
        player1.Victory();
        player2.Victory();
        
        VictoryEvent.Invoke();
    }
}
