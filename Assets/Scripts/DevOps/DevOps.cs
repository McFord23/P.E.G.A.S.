using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DevOps : MonoBehaviour
{
    Player player;
    public GameObject barriers;

    public Canvas canvas;
    public GameObject menu;
    public GameObject pauseMenu;
    public GameObject victoryMenu;
    public GameObject deadMenu;

    public AudioSource clickSound;
    public MusicController musicController;
    AudioSource gameMusic;
    AudioSource victoryMusic;

    List<Vector2> points = new List<Vector2>();
    public Vector2 point1;
    public Vector2 point2;
    int i = 0;

    public bool mode = false;

    HUD hud;
    InfoController infoController;
    DeathIndicator deathIndicator;
    SpawnChangeMessege spawnChangeMessege;

    public UnityEvent InCannonEvent;
    public UnityEvent OutCannonEvent;

    void Start()
    {
        player = GetComponentInParent<Player>();
        
        pauseMenu = menu.transform.Find("Pause Menu").gameObject;
        victoryMenu = menu.transform.Find("Victory Menu").gameObject;
        deadMenu = menu.transform.Find("Dead Menu").gameObject;

        gameMusic = musicController.flyingMusic;
        victoryMusic = musicController.victoryMusic;

        infoController = GetComponent<InfoController>();
        hud = GetComponent<HUD>();
        deathIndicator = GetComponent<DeathIndicator>();
        spawnChangeMessege = GetComponent<SpawnChangeMessege>();

        points.Add(player.spawnPos);
        points.Add(point1);
        points.Add(point2);
    }

    void FixedUpdate()
    {
        // Pull        
        if (mode && player.pullClick && (pauseMenu.activeSelf || deadMenu.activeSelf || victoryMenu.activeSelf))
        {
            player.rb.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void Update()
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
                DevModeActive(false);
                hud.enabled = false;
            }
            else
            {
                DevModeActive(true);
                if (infoController.enabled) infoController.enabled = false;
                hud.enabled = true;
            }
        }

        if (mode)
        {
            if (Input.GetKeyDown(KeyCode.F4))
            {
                player.godnessMode = !player.godnessMode;
                if (player.godnessMode) player.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                else player.rb.constraints = RigidbodyConstraints2D.None;
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                if (i == 0)
                {
                    player.sprite.enabled = true;
                    player.rb.gravityScale = 1f;
                    OutCannonEvent.Invoke();
                }

                if (i == (points.Count - 1))
                {
                    player.moveState = Player.MoveState.Loaded;
                    player.sprite.enabled = false;
                    player.rb.gravityScale = 0f;
                    InCannonEvent.Invoke();
                }
                else
                {
                    player.moveState = Player.MoveState.FreeFall;
                }

                if (i < points.Count - 1) i++;
                else i = 0;

                player.transform.position = points[i];
                
                
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                player.spawnPos = points[i];
                spawnChangeMessege.enabled = true;
                Invoke("OffMessege", 1f);
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                barriers.SetActive(!barriers.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.V) && (player.moveState != Player.MoveState.Dead) && !pauseMenu.activeSelf)
            {
                if (victoryMenu.activeSelf)
                {
                    victoryMusic.Stop();
                    clickSound.Play();
                    gameMusic.Play();

                    victoryMenu.SetActive(false);
                    menu.SetActive(false);
                    player.Resume();
                }
                else
                {
                    menu.SetActive(true);
                    victoryMenu.SetActive(true);
                    gameMusic.Stop();
                    clickSound.Play();
                    victoryMusic.Play();

                    player.Victory();
                }
            }
        }
    }

    void DevModeActive(bool check)
    {
        mode = player.godnessMode = check;
        canvas.GetComponent<CanvasGroup>().alpha = (check) ? 0f : 1f;
    }

    public void Reset()
    {
        if (player.spawnPos == points[0]) player.LoadInCannon();
        else player.moveState = Player.MoveState.FreeFall;
    }

    void OffMessege()
    {
        spawnChangeMessege.enabled = false;
    }
}
