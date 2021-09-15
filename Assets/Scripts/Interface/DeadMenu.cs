using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    GameObject deadMenu;
    GameObject menu;
    UnityEvent MenuDisabledEvent;

    public PlayersController playersController;

    void Start()
    {
        deadMenu = transform.gameObject;
        menu = transform.parent.gameObject;
        MenuDisabledEvent = menu.GetComponent<MenuController>().MenuDisabledEvent;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            Retry();
        }
    }

    public void Retry()
    {
        playersController.Reset();
        MenuDisabledEvent.Invoke();
        deadMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void Exit()
    {
        Save.Player1.live = true;
        Save.Player2.live = true;
        SceneManager.LoadScene("Main Menu");
    }
}
