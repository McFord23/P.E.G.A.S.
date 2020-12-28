﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenu;
    GameObject menu;
    UnityEvent MenuDisabledEvent;

    public Player player;

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
        player.Resume();
        MenuDisabledEvent.Invoke();
        pauseMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
