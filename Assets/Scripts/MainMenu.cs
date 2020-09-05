using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
