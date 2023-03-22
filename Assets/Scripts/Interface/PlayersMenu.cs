using Enums;
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

        player1Menu.layout = Save.players[0].controlLayout;
        player2Menu.layout = Save.players[1].controlLayout;
    }

    void Start()
    {
        ChangePlayer1Layout();
        ChangePlayer2Layout();

        player1Menu.ChangeCharacter(Save.players[0].character);
        player2Menu.ChangeCharacter(Save.players[1].character);

        if (Save.gameMode != GameMode.Single) AddPlayer();
        else KickPlayer();

        UpdateGamepadStatus();
    }

    public void AddPlayer()
    {
        Save.gameMode = GameMode.Host;
        menu.UpdatePlayersIcon();
        addPlayer.SetActive(false);
        player2Panel.SetActive(true);
    }

    public void KickPlayer()
    {
        Save.gameMode = GameMode.Single;
        menu.UpdatePlayersIcon();
        player2Panel.SetActive(false);
        addPlayer.SetActive(true);
    }

    public void ChangeCharacter()
    {
        if (Save.players[0].character == Character.Celestia)
        {
            Save.players[0].character = Character.Luna;
            Save.players[1].character = Character.Celestia;

            player1Menu.ChangeCharacter(Character.Luna);
            player2Menu.ChangeCharacter(Character.Celestia);
        }
        else
        {
            Save.players[0].character = Character.Celestia;
            Save.players[1].character = Character.Luna;

            player1Menu.ChangeCharacter(Character.Celestia);
            player2Menu.ChangeCharacter(Character.Luna);
        }
    }

    public void ChangeGamepad()
    {
        (Save.players[0].gamepad, Save.players[1].gamepad) = (Save.players[1].gamepad, Save.players[0].gamepad);
        (player1GamepadImage.sprite, player2GamepadImage.sprite) = (player2GamepadImage.sprite, player1GamepadImage.sprite);
    }

    public void UpdateGamepadStatus()
    {
        if (Save.players[0].gamepad == 1)
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
        Save.players[0].controlLayout = player1Menu.layout;
        player2Menu.BlockIndex(player1Menu.layout);
    }

    public void ChangePlayer2Layout()
    {
        Save.players[1].controlLayout = player2Menu.layout;
        player1Menu.BlockIndex(player2Menu.layout);
    }
}
