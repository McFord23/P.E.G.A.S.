using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    Player player;
    
    public GameObject menu;
    public GameObject pauseMenu;
    public GameObject deadMenu;
    GameObject victoryMenu;

    float flapTimeTrigger = 0.375f;

    void Start()
    {
        player = GetComponent<Player>();

        pauseMenu = menu.transform.Find("PauseMenu").gameObject;
        deadMenu = menu.transform.Find("DeadMenu").gameObject;
        victoryMenu = menu.transform.Find("VictoryMenu").gameObject;
    }

    void Update()
    {
        if ((player.moveState != Player.MoveState.Dead) && !pauseMenu.activeSelf && !victoryMenu.activeSelf)
        {
            //Keyboard
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Flap();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                flapTimeTrigger -= Time.deltaTime;
                if (flapTimeTrigger <= 0)
                {
                    player.Hover();
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                player.rb.gravityScale = 1f;
                flapTimeTrigger = 0.375f;
                if (player.moveState == Player.MoveState.Hover) player.FreeFall();
            }

            //Mouse
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                player.Flap();
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                flapTimeTrigger -= Time.deltaTime;
                if (flapTimeTrigger <= 0)
                {
                    player.Hover();
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                flapTimeTrigger = 0.375f;
                if (player.moveState == Player.MoveState.Hover) player.FreeFall();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !pauseMenu.activeSelf && !victoryMenu.activeSelf)
        {
            deadMenu.GetComponent<DeadMenu>().Retry();
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
