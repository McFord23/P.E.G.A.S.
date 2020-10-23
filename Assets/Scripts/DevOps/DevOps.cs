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

    public AudioSource clickSound;
    public MusicController musicController;
    AudioSource gameMusic;
    AudioSource victoryMusic;

    List<Vector3> points = new List<Vector3>();
    public Vector3 point1;
    public Vector3 point2;
    int i = 0;

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

        gameMusic = musicController.flyingMusic;
        victoryMusic = musicController.victoryMusic;

        infoController = GetComponent<InfoController>();
        hud = GetComponent<HUD>();
        deathIndicator = GetComponent<DeathIndicator>();

        points.Add(player.transform.position);
        points.Add(point1);
        points.Add(point2);
    }

    void FixedUpdate()
    {
        // Pull        
        if (mode && player.pullClick && (pauseMenu.activeSelf || deadMenu.activeSelf || victoryMenu.activeSelf))
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

            if (Input.GetKeyDown(KeyCode.F5))
            {
                if (i < points.Count - 1) i++;
                else i = 0;
                player.transform.position = points[i]; 
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                player.spawnPos = points[i];
            }

            if (Input.GetKeyDown(KeyCode.V) && (player.moveState != Player.MoveState.Dead) && !pauseMenu.activeSelf)
            {
                if (victoryMenu.activeSelf)
                {
                    victoryMusic.Stop();
                    clickSound.Play();
                    gameMusic.Play();

                    victoryMenu.SetActive(false);
                    player.Resume();
                }
                else
                {
                    victoryMenu.SetActive(true);
                    gameMusic.Stop();
                    clickSound.Play();
                    victoryMusic.Play();

                    player.Pause();
                }
            }
        }
    }

    public void Active(bool check)
    {
        mode = player.godnessMode = check;
        menu.GetComponent<CanvasGroup>().alpha = (check) ? 0f : 1f;
    }
}
