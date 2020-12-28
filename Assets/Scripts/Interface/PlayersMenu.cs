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
    public PlayerMenu player1Menu;
    public PlayerMenu player2Menu;

    void Start()
    {
        menu = GetComponentInParent<MenuController>();
        addPlayer = transform.Find("Add Player").gameObject;
        player2Panel = transform.Find("Player #2").gameObject;

        if (Save.TogetherMode) AddPlayer();
        else KickPlayer();

        player1Menu.layout = "mouse";
        player2Menu.layout = "numpad";
        AnchorPlayer1Layout();
        AnchorPlayer2Layout();
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

    public void Swap()
    {
        var set1 = player1Menu.layout;
        var set2 = player2Menu.layout;

        player1Menu.SetLayout(set2);
        player1Menu.SetLimiter(set1);
        player2Menu.SetLayout(set1);
        player2Menu.SetLimiter(set2);
    }

    public void AnchorPlayer1Layout()
    {
        player2Menu.SetLimiter(player1Menu.layout);
    }

    public void AnchorPlayer2Layout()
    {
        player1Menu.SetLimiter(player2Menu.layout);
    }
}
