using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Player player;
    public Barriers barriers;
    public GameObject pauseMenu;
    private Button fakeButton;

    public AudioSource clickSound;
    public AudioSource gameMusic;
    public AudioSource pauseMusic;


    private void Start()
    {
        fakeButton = pauseMenu.GetComponent<Button>();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        player.Pause();
        barriers.Pause(true);

        clickSound.Play();
        gameMusic.Stop();
        pauseMusic.Play();
    }

    public void Resume()
    {
        clickSound.Play();
        pauseMusic.Stop();
        gameMusic.Play();

        pauseMenu.SetActive(false);
        player.Resume();
        barriers.Pause(false);

        fakeButton.Select();
    }

    public void Exit()
    {
        clickSound.Play();
        Time.timeScale = 0f;
        SceneManager.LoadScene("Menu");
    }
}
