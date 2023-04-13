using Enums;
using UnityEngine;
using UnityEngine.UI;

public class PlayersMenu : MonoBehaviour
{
    private MenuController menu;
    private GameObject coopSubmenu;
    private GameObject networkSubmenu;

    private PlayerSubmenu player1Submenu;
    private Text p1;
    
    private RectTransform backMain;
    private GameObject backCoop;

    private PlayerSubmenu player2Submenu;
    private Text p2;
    private Text player2Title;
    
    private GameObject localBannishButton;
    private GameObject networkBannishButton;
    private GameObject quitButton;

    public Sprite[] controlLayoutSprites;
    public Sprite[] gamepadSprites;
    private Image player1GamepadImage;
    private Image player2GamepadImage;

    private void Awake()
    {
        menu = GetComponentInParent<MenuController>();
        coopSubmenu = transform.Find("Coop Submenu").gameObject;
        networkSubmenu = transform.Find("Network Submenu").gameObject;
        
        player1Submenu = transform.Find("Player #1").GetComponent<PlayerSubmenu>();
        p1 = player1Submenu.transform.Find("P1Text").GetComponent<Text>();
        
        backMain = player1Submenu.transform.Find("Back Main").GetComponent<RectTransform>();
        backCoop = player1Submenu.transform.Find("Back Coop").gameObject;

        player2Submenu = transform.Find("Player #2").GetComponent<PlayerSubmenu>();
        player2Title = player2Submenu.transform.Find("Title").GetComponent<Text>();
        p2 = player2Submenu.transform.Find("P2Text").GetComponent<Text>();
        
        localBannishButton = player2Submenu.transform.Find("Local Bannish").gameObject;
        networkBannishButton = player2Submenu.transform.Find("Network Bannish").gameObject;
        quitButton = player2Submenu.transform.Find("Quit").gameObject;

        player1Submenu.Initialize();
        player2Submenu.Initialize();

        player1GamepadImage = transform.Find("Player #1/Gamepad").GetComponent<Image>();
        player2GamepadImage = transform.Find("Player #2/Gamepad").GetComponent<Image>();

        player1Submenu.layout = Global.players[0].controlLayout;
        player2Submenu.layout = Global.players[1].controlLayout;
    }

    void Start()
    {
        ChangePlayer1Layout();
        ChangePlayer2Layout();

        player1Submenu.ChangeCharacter(Global.players[0].character);
        player2Submenu.ChangeCharacter(Global.players[1].character);

        UpdateGamepadStatus();
    }

    public void LocalCoop()
    {
        Global.gameMode = GameMode.LocalCoop;
        Global.playerAmmount = 2;
        coopSubmenu.SetActive(false);

        UpdatePlayersSubmenu();
    }

    public void NetworkCoop()
    {
        coopSubmenu.SetActive(false);
        networkSubmenu.SetActive(true);
        player2Title.text = "Network Coop";

        UpdateBackButtons(false);
    }

    public void LocalBannish()
    {
        Global.gameMode = GameMode.Single;
        Global.playerAmmount = 1;
        coopSubmenu.SetActive(true);
        Bannish();
    }

    public void Bannish()
    {
        menu.UpdatePlayersIcon();
        UpdatePlayersSubmenu();
    }

    public void BackCoop()
    {
        networkSubmenu.SetActive(false);
        coopSubmenu.SetActive(true);

        UpdateBackButtons(true);
    }

    private void UpdateBackButtons(bool toOne)
    {
        if (toOne)
        {
            backMain.anchoredPosition = new Vector2(0, -311);
            backCoop.SetActive(false);
        }
        else
        {
            backMain.anchoredPosition = new Vector2(0, -287);
            backCoop.SetActive(true);
        }
    }

    private void UpdateKickButton()
    {
        switch (Global.gameMode)
        {
            case GameMode.LocalCoop:
                quitButton.SetActive(false);
                networkBannishButton.SetActive(false);
                localBannishButton.SetActive(true);
                break;

            case GameMode.Host:
                quitButton.SetActive(false);
                localBannishButton.SetActive(false);
                networkBannishButton.SetActive(true);
                break;
                
            case GameMode.Client:
                localBannishButton.SetActive(false);
                networkBannishButton.SetActive(false);
                quitButton.SetActive(true);
                break;
        }
    }

    public void UpdatePlayersSubmenu()
    {
        switch (Global.gameMode)
        {
            case GameMode.LocalCoop:
                player2Title.text = "Local Coop";
                p1.text = "Player 1";
                p2.text = "Player 2";

                player2Submenu.ShowButton(true);
                player1Submenu.ShowButton(true);

                player2Submenu.gameObject.SetActive(true);
                break;

            case GameMode.Host when Global.playerAmmount == 2:
                player2Title.text = "Network Coop";
                p1.text = "You";
                p2.text = "Sister";

                player2Submenu.ShowButton(false);
                player1Submenu.ShowButton(true);

                player2Submenu.gameObject.SetActive(true);
                break;

            case GameMode.Client when Global.playerAmmount == 2:
                player2Title.text = "Network Coop";
                p1.text = "Sister";
                p2.text = "You";

                player2Submenu.ShowButton(true);
                player1Submenu.ShowButton(false);

                player2Submenu.gameObject.SetActive(true);
                break;

            default:
                player2Title.text = "Local Coop";
                p1.text = "Player 1";
                p2.text = "Player 2";

                player1Submenu.ShowButton(true);

                player1Submenu.gameObject.SetActive(true);
                player2Submenu.gameObject.SetActive(false);
                break;
        }

        UpdateKickButton();
        UpdateGamepadStatus();
        menu.UpdatePlayersIcon();
    }

    public void ChangeCharacter()
    {
        if (Global.players[0].character == Character.Celestia)
        {
            Global.players[0].character = Character.Luna;
            Global.players[1].character = Character.Celestia;

            player1Submenu.ChangeCharacter(Character.Luna);
            player2Submenu.ChangeCharacter(Character.Celestia);
        }
        else
        {
            Global.players[0].character = Character.Celestia;
            Global.players[1].character = Character.Luna;

            player1Submenu.ChangeCharacter(Character.Celestia);
            player2Submenu.ChangeCharacter(Character.Luna);
        }
    }

    public void ChangeGamepad()
    {
        (Global.players[0].gamepad, Global.players[1].gamepad) = (Global.players[1].gamepad, Global.players[0].gamepad);
        (player1GamepadImage.sprite, player2GamepadImage.sprite) = (player2GamepadImage.sprite, player1GamepadImage.sprite);
    }

    public void UpdateGamepadStatus()
    {
        if (Global.gameMode == GameMode.Single)
        {
            if (Gamepad.gamepad1) player1GamepadImage.sprite = gamepadSprites[0];
            else player1GamepadImage.sprite = gamepadSprites[3];
        }
        else
        {
            if (Gamepad.gamepad1) player1GamepadImage.sprite = gamepadSprites[1];
            else player1GamepadImage.sprite = gamepadSprites[3];

            if (Gamepad.gamepad2) player2GamepadImage.sprite = gamepadSprites[2];
            else player2GamepadImage.sprite = gamepadSprites[3];
        }
    }

    public void ChangePlayer1Layout()
    {
        Global.players[0].controlLayout = player1Submenu.layout;
        player2Submenu.BlockIndex(player1Submenu.layout);
    }

    public void ChangePlayer2Layout()
    {
        Global.players[1].controlLayout = player2Submenu.layout;
        player1Submenu.BlockIndex(player2Submenu.layout);
    }
}
