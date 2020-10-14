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

    public AudioSource clickSound;
    public MusicController musicController;

    Button fakeButton;

    void Start()
    {
        fakeButton = pauseMenu.GetComponent<Button>();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        player.Pause();
        barriers.Pause(true);

        clickSound.Play();
        musicController.flyingMusic.Pause();
        musicController.pauseMusic.Play();
    }

    public void Resume()
    {
        fakeButton.Select();

        clickSound.Play();
        musicController.pauseMusic.Stop();
        musicController.flyingMusic.Play();

        pauseMenu.SetActive(false);
        player.Resume();
        barriers.Pause(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
