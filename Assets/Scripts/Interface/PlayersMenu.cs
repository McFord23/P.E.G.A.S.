using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersMenu : MonoBehaviour
{
    MenuController menu;
    GameObject addPlayer;
    GameObject player2Panel;

    public Sprite[] controlLayoutSprites;
    public Sprite[] gamepadSprites;
    Image player1GamepadImage;
    Image player2GamepadImage;

    public PlayerMenu player1Menu;
    public PlayerMenu player2Menu;

    private void Awake()
    {
        menu = GetComponentInParent<MenuController>();
        addPlayer = transform.Find("Add Player").gameObject;
        player2Panel = transform.Find("Player #2").gameObject;

        player1GamepadImage = transform.Find("Player #1/Gamepad").GetComponent<Image>();
        player2GamepadImage = transform.Find("Player #2/Gamepad").GetComponent<Image>();

        player1Menu.layout = Save.Player1.controlLayout;
        player2Menu.layout = Save.Player2.controlLayout;
    }

    void Start()
    {
        ChangePlayer1Layout();
        ChangePlayer2Layout();

        player1Menu.ChangeCharacter(Save.Player1.character);
        player2Menu.ChangeCharacter(Save.Player2.character);

        if (Save.TogetherMode) AddPlayer();
        else KickPlayer();

        UpdateGamepadStatus();
    }

    public void AddPlayer()
    {
        Save.TogetherMode = true;
        menu.UpdatePlayersIcon();
        addPlayer.SetActive(false);
        player2Panel.SetActive(true);
    }

    public void KickPlayer()
    {
        Save.TogetherMode = false;
        menu.UpdatePlayersIcon();
        player2Panel.SetActive(false);
        addPlayer.SetActive(true);
    }

    public void ChangeCharacter()
    {
        if (Save.Player1.character == "Celestia")
        {
            Save.Player1.character = "Luna";
            Save.Player2.character = "Celestia";

            player1Menu.ChangeCharacter("Luna");
            player2Menu.ChangeCharacter("Celestia");
        }
        else
        {
            Save.Player1.character = "Celestia";
            Save.Player2.character = "Luna";

            player1Menu.ChangeCharacter("Celestia");
            player2Menu.ChangeCharacter("Luna");
        }
    }

    public void ChangeGamepad()
    {
        var gamepad = Save.Player1.gamepad;
        Save.Player1.gamepad = Save.Player2.gamepad;
        Save.Player2.gamepad = gamepad;

        var gamepadImage = player1GamepadImage.sprite;
        player1GamepadImage.sprite = player2GamepadImage.sprite;
        player2GamepadImage.sprite = gamepadImage;
    }

    public void UpdateGamepadStatus()
    {
        if (Save.Player1.gamepad == 1)
        {
            if (Gamepad.gamepad1) player1GamepadImage.sprite = gamepadSprites[0];
            else player1GamepadImage.sprite = gamepadSprites[1];

            if (Gamepad.gamepad2) player2GamepadImage.sprite = gamepadSprites[2];
            else player2GamepadImage.sprite = gamepadSprites[3];
        }
        else
        {
            if (Gamepad.gamepad1) player2GamepadImage.sprite = gamepadSprites[0];
            else player2GamepadImage.sprite = gamepadSprites[1];

            if (Gamepad.gamepad2) player1GamepadImage.sprite = gamepadSprites[2];
            else player1GamepadImage.sprite = gamepadSprites[3];
        }
    }

    public void ChangePlayer1Layout()
    {
        Save.Player1.controlLayout = player1Menu.layout;
        player2Menu.BlockIndex(player1Menu.layout);
    }

    public void ChangePlayer2Layout()
    {
        Save.Player2.controlLayout = player2Menu.layout;
        player1Menu.BlockIndex(player2Menu.layout);
    }
}
