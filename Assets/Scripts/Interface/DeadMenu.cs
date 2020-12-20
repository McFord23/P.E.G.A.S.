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

    public Player player;

    void Start()
    {
        deadMenu = transform.gameObject;
        menu = transform.parent.gameObject;
        MenuDisabledEvent = menu.GetComponent<MenuController>().MenuDisabledEvent;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            Retry();
        }
    }

    public void Retry()
    {
        player.Reset();
        MenuDisabledEvent.Invoke();
        deadMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
