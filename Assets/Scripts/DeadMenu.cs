using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
    public Player player;
    public GameObject deadMenu;
    public Barriers barriers;
    private Button fakeButton;

    public AudioSource clickSound;

    public void Retry()
    {
        clickSound.Play();
        deadMenu.SetActive(false);

        player.Reset();
        barriers.Reset();
        fakeButton.Select();
    }

    public void Exit()
    {
        clickSound.Play();
        Time.timeScale = 0f;
        SceneManager.LoadScene("Menu");
    }

    void Start()
    {
        fakeButton = deadMenu.GetComponent<Button>();
    }
}
