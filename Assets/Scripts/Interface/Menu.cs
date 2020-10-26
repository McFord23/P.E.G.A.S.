using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    GameObject menu;
    GameObject deadMenu;
    GameObject pauseMenu;
    GameObject victoryMenu;

    bool value = true;

    public UnityEvent MenuActiveChangeEvent;

    void Start()
    {
        menu = transform.gameObject;
        deadMenu = transform.Find("DeadMenu").gameObject;
        pauseMenu = transform.Find("PauseMenu").gameObject;
        victoryMenu = transform.Find("VictoryMenu").gameObject;

        deadMenu.SetActive(false);
        pauseMenu.SetActive(false);
        victoryMenu.SetActive(false);
        menu.SetActive(false);
        MenuActiveChangeEvent.Invoke();
    }

    public void Pause()
    {
        menu.SetActive(true);
        MenuActiveChangeEvent.Invoke();
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        MenuActiveChangeEvent.Invoke();
        menu.SetActive(false);
    }

    public void Dead()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
            MenuActiveChangeEvent.Invoke();
        }
        deadMenu.SetActive(true);
    }

    public void Retry()
    {
        if (deadMenu.activeSelf) MenuActiveChangeEvent.Invoke();
        deadMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void Victory()
    {
        menu.SetActive(true);
        MenuActiveChangeEvent.Invoke();
        victoryMenu.SetActive(true);
    }

    public void ToBeContinued()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CurcorLockModeChange()
    {
        value = !value;
        Cursor.lockState = (value) ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
