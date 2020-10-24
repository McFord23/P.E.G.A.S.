using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    Player player;
    
    public GameObject menu;
    GameObject pauseMenu;
    GameObject deadMenu;
    GameObject victoryMenu;

    void Start()
    {
        player = GetComponent<Player>();

        pauseMenu = menu.transform.Find("PauseMenu").gameObject;
        deadMenu = menu.transform.Find("DeadMenu").gameObject;
        victoryMenu = menu.transform.Find("VictoryMenu").gameObject;
    }

    void Update()
    {
        if ((player.moveState == Player.MoveState.Loaded) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            player.Shoot();
        }
        
        if ((player.moveState != Player.MoveState.Dead) && !pauseMenu.activeSelf && !victoryMenu.activeSelf)
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.Reset();
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && (player.moveState != Player.MoveState.Dead) && !victoryMenu.activeSelf)
        {
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.GetComponent<PauseMenu>().Pause();
            }
            else
            {
                pauseMenu.GetComponent<PauseMenu>().Resume();
            }
        }
    }
}
