using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public Player player;

    public GameObject menu;
    GameObject victoryMenu;
    GameObject deadMenu;
    GameObject pauseMenu;
    
    public AudioSource clickSound;
    public MusicController musicController;

    private void Start()
    {
        pauseMenu = menu.transform.Find("PauseMenu").gameObject;
        deadMenu = menu.transform.Find("DeadMenu").gameObject;
        victoryMenu = menu.transform.Find("VictoryMenu").gameObject;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (deadMenu.activeSelf) deadMenu.SetActive(false);
            if (pauseMenu.activeSelf) pauseMenu.SetActive(false);

            victoryMenu.SetActive(true);
            player.Pause();
            clickSound.Play();
            musicController.flyingMusic.Stop();
            musicController.victoryMusic.Play();
        }
    }
}
