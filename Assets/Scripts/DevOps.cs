using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevOps : MonoBehaviour
{
    Player player;
    
    public GameObject menu;
    public GameObject pauseMenu;
    public GameObject victoryMenu;
    public GameObject deadMenu;

    public bool mode = false;

    HUD hud;
    InfoController infoController;
    DeathIndicator deathIndicator;

    void Start()
    {
        player = GetComponentInParent<Player>();
        
        pauseMenu = menu.transform.Find("PauseMenu").gameObject;
        victoryMenu = menu.transform.Find("VictoryMenu").gameObject;
        deadMenu = menu.transform.Find("DeadMenu").gameObject;

        infoController = GetComponent<InfoController>();
        hud = GetComponent<HUD>();
        deathIndicator = GetComponent<DeathIndicator>();
    }

    void FixedUpdate()
    {
        // Pull        
        if (mode && player.pullClick && pauseMenu.activeSelf)
        {
            player.rb.constraints = RigidbodyConstraints2D.None;
            player.rb.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void Update()
    {  
        // BOOM
        deathIndicator.enabled = (player.godnessMode && player.deathIndicator);

        //Controls
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (hud.enabled) hud.enabled = false;
            infoController.enabled = !infoController.enabled;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (infoController.enabled) infoController.enabled = false;
            hud.enabled = !hud.enabled;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (mode)
            {
                Active(false);
                hud.enabled = false;
            }
            else
            {
                Active(true);
                if (infoController.enabled) infoController.enabled = false;
                hud.enabled = true;
            }
        }

        if (mode)
        {
            if (Input.GetKeyDown(KeyCode.F4))
            {
                player.godnessMode = !player.godnessMode;
            }

            if (Input.GetKeyDown(KeyCode.V) && (player.moveState != Player.MoveState.Dead) && !pauseMenu.activeSelf)
            {
                if (victoryMenu.activeSelf)
                {
                    victoryMenu.SetActive(false);
                    player.Resumed();
                }
                else
                {
                    victoryMenu.SetActive(true);
                    player.Paused();
                }
            }
        }
    }

    public void Active(bool modeAndGodness)
    {
        mode = player.godnessMode = modeAndGodness;
        menu.GetComponent<CanvasGroup>().alpha = (modeAndGodness) ? 0.5f : 1f;
    }
}
