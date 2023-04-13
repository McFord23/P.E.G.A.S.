﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DeadMenu : MonoBehaviour
{
    private GameObject deadMenu;
    private GameObject menu;
    private UnityEvent MenuDisabledEvent;
    
    [SerializeField]
    private PlayersManager playersManager;

    private void Start()
    {
        deadMenu = transform.gameObject;
        menu = transform.parent.gameObject;
        MenuDisabledEvent = menu.GetComponent<MenuController>().MenuDisabledEvent;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            Retry();
        }
    }

    public void Retry()
    {
        playersManager.Reset();
        MenuDisabledEvent.Invoke();
        deadMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void Exit()
    {
        Global.players[0].live = true;
        Global.players[1].live = true;
        SceneManagerAdapter.LoadScene("Main Menu");
    }
}
