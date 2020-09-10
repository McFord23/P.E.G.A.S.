using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public GameObject victoryMenu;
    public Player player;

    public AudioSource clickSound;
    public AudioSource gameMusic;
    public AudioSource victoryMusic;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameMusic.Stop();
            clickSound.Play();
            victoryMusic.Play();

            victoryMenu.SetActive(true);
            player.Paused();
        }
    }
}
