using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    bool togetherMode = false;

    public GameObject player1GameObject;
    public GameObject player2GameObject;
    Player player1;
    Player player2;
    PlayerController player1Controller;
    PlayerController player2Controller;
    
    public PlayersMenu playersMenu;
    string player1Layout;
    string player2Layout;

    float gas;
    float rotate;
    float shoot;

    void Start()
    {
        player1 = player1GameObject.GetComponent<Player>();
        player2 = player2GameObject.GetComponent<Player>();
        player1Controller = player1GameObject.GetComponent<PlayerController>();
        player2Controller = player2GameObject.GetComponent<PlayerController>();

        player1Layout = "mouse";
        player2Layout = "numpad";

        //ChangeSet("Player 1");
        //ChangeSet("Player 2");

        //print("Check: " + Input.GetJoystickNames().Length);
    }

    void Update()
    {
        if (togetherMode)
        {
            UpdateLayout(player1Controller);
            UpdateLayout(player2Controller);
        }
        else
        {
            UpdateLayout(player1Controller);
        }
    }

    public void TogetherMode(bool var)
    {
        togetherMode = var;
    }

    public void ChangeCharacter()
    {

    }

    public void ChangeLayout(string player)
    {
        if (player == "Player 1") player1Layout = playersMenu.player1Menu.layout;
        else if (player == "Player 2") player2Layout = playersMenu.player1Menu.layout;
    }

    public void UpdateLayout(PlayerController player)
    {
        string layout = (player == player1Controller) ? player1Layout : player2Layout;
        switch (layout)
        {
            case "mouse":
                gas = Input.GetKey(KeyCode.Mouse0) ? 1f : 0f;
                rotate = Save.MouseSensitivity * Input.GetAxis("Rotate-Mouse");
                shoot = Input.GetKey(KeyCode.Mouse1) ? 1f : 0f;
                player.SetInput(gas, rotate, shoot);
                break;
           
            case "wasd":
                gas = Input.GetKey(KeyCode.LeftShift) ? 1f : 0f;
                rotate = Save.KeyboardSensitivity * Input.GetAxis("Rotate-WASD");
                shoot = Input.GetKey(KeyCode.LeftControl) ? 1f : 0f;
                player.SetInput(gas, rotate, shoot);
                break;
            
            case "ijkl":
                gas = Input.GetKey(KeyCode.RightShift) ? 1f : 0f;
                rotate = Save.KeyboardSensitivity * Input.GetAxis("Rotate-IJKL");
                shoot = Input.GetKey(KeyCode.RightControl) ? 1f : 0f;
                player.SetInput(gas, rotate, shoot);
                break;
           
            case "arrow":
                gas = Input.GetKey(KeyCode.RightShift) ? 1f : 0f;
                rotate = Save.KeyboardSensitivity * Input.GetAxis("Rotate-Arrow");
                shoot = Input.GetKey(KeyCode.RightControl) ? 1f : 0f;
                player.SetInput(gas, rotate, shoot);
                break;
           
            case "numpad":
                gas = Input.GetKey(KeyCode.Space) ? 1f : 0f;
                rotate = Save.KeyboardSensitivity * Input.GetAxis("Rotate-Numpad");
                shoot = Input.GetKey(KeyCode.LeftShift) ? 1f : 0f;
                player.SetInput(gas, rotate, shoot);
                break;
           
            case "gamepad1":
                gas = Input.GetAxis("Gas-Gamepad 1");
                rotate = Save.GamepadSensitivity * Input.GetAxis("Rotate-Gamepad 1");
                shoot = Input.GetAxis("Shoot-Gamepad 1");
                player.SetInput(gas, rotate, shoot);
                break;
           
            case "gamepad2":
                gas = Input.GetAxis("Gas-Gamepad 2");
                rotate = Save.GamepadSensitivity * Input.GetAxis("Rotate-Gamepad 2");
                shoot = Input.GetAxis("Shoot-Gamepad 2");
                player.SetInput(gas, rotate, shoot);
                break;
        }
    }

    public void OffInput(PlayerController player)
    {
        gas = 0f;
        rotate = 0f;
        shoot = 0f;
        player.SetInput(gas, rotate, shoot);
    }
}
