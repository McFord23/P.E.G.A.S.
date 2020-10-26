using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerKeyboardController : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if ((player.moveState != Player.MoveState.Dead) && (player.moveState != Player.MoveState.Paused))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Flap();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                player.Flap();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && (player.moveState != Player.MoveState.Paused))
        {
            player.Reset();
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && (player.moveState != Player.MoveState.Dead))
        {
            if (player.moveState != Player.MoveState.Paused) player.Pause("AFK");
            else player.Resume();
        }
    }
}
