using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSound;

    public void Play()
    {
        SceneManager.LoadScene("Game");
        clickSound.Play();
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        clickSound.Play();
        Application.Quit();
    }
}
