using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Player player;
    public GameObject pauseMenu;
    public new CameraFollow camera;
    private Button fakeButton;

    public AudioSource clickSound;
    public AudioSource gameMusic;
    public AudioSource pauseMusic;

    public void Resume()
    {
        clickSound.Play();
        pauseMusic.Stop();
        gameMusic.Play();

        pauseMenu.SetActive(false);
        player.Resumed();
        //Time.timeScale = 1f;
        fakeButton.Select();
    }

    public void Exit()
    {
        clickSound.Play();
        Time.timeScale = 0f;
        SceneManager.LoadScene("Menu");
    }

    private void Start()
    {
        fakeButton = pauseMenu.GetComponent<Button>();
    }

    private void Update()
    {
        if (pauseMenu.activeInHierarchy)
        {
            camera.FocusOnPlayer();
        }
    }
}
