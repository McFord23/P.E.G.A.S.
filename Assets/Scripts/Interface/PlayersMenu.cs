using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMenu : MonoBehaviour
{
    //GameObject player1;
    //GameObject player2;

    public Sprite[] setSprites;
    public PlayerMenu player1Menu;
    public PlayerMenu player2Menu;

    void Start()
    {
        player1Menu.controllSet = "mouse";
        player2Menu.controllSet = "numpad";

        AnchorPlayer1Set();
        AnchorPlayer2Set();
    }

    public void AnchorPlayer1Set()
    {
        player2Menu.SetLimiter(player1Menu.controllSet);
    }

    public void AnchorPlayer2Set()
    {
        player1Menu.SetLimiter(player2Menu.controllSet);
    }
}
