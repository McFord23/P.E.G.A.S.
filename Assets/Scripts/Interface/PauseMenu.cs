using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenu;
    GameObject menu;
    UnityEvent MenuDisabledEvent;

    public PlayersController playersController;

    void Start()
    {
        pauseMenu = transform.gameObject;
        menu = transform.parent.gameObject;
        MenuDisabledEvent = menu.GetComponent<MenuController>().MenuDisabledEvent;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Resume();
        }
    }

    public void Resume()
    {
        playersController.Resume();
        MenuDisabledEvent.Invoke();
        pauseMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void Exit()
    {
        Save.Player1.live = true;
        Save.Player2.live = true;
        SceneManager.LoadScene("Main Menu");
    }
}
