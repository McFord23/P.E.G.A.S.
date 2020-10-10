using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
    public Player player;
    public Barriers barriers;

    public GameObject deadMenu;
    private Button fakeButton;

    public AudioSource clickSound;

    void Start()
    {
        fakeButton = deadMenu.GetComponent<Button>();
    }

    public void Retry()
    {
        clickSound.Play();
        
        if (deadMenu.activeSelf) fakeButton.Select();
        deadMenu.SetActive(false);

        player.Reset();
        barriers.Reset();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
