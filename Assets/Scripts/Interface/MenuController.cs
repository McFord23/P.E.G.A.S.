using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    GameObject menu;
    GameObject deadMenu;
    GameObject pauseMenu;
    GameObject victoryMenu;
    GameObject settingsMenu;
    GameObject playersMenu;

    string resumeMenu;

    Toggle resumeMark;
    Toggle settingsMark;
    Toggle playersMark;
    
    Image resumeButton;
    public Sprite resumeButtonOn;
    public Sprite resumeButtonOff;

    Image playersButton;
    public Sprite playersButtonOff;
    public Sprite playersButtonOn;

    public UnityEvent MenuEnabledEvent;
    public UnityEvent MenuDisabledEvent;

    void Awake()
    {
        menu = transform.gameObject;
        deadMenu = transform.Find("Dead Menu").gameObject;
        pauseMenu = transform.Find("Pause Menu").gameObject;
        victoryMenu = transform.Find("Victory Menu").gameObject;
        settingsMenu = transform.Find("Settings Menu").gameObject;
        playersMenu = transform.Find("Players Menu").gameObject;

        deadMenu.SetActive(false);
        pauseMenu.SetActive(false);
        victoryMenu.SetActive(false);
        menu.SetActive(false);
        MenuDisabledEvent.Invoke();

        resumeMark = transform.Find("Resume Mark").gameObject.GetComponent<Toggle>();
        settingsMark = transform.Find("Settings Mark").gameObject.GetComponent<Toggle>();
        playersMark = transform.Find("Players Mark").gameObject.GetComponent<Toggle>();

        resumeButton = transform.Find("Resume Icon").gameObject.GetComponent<Image>();
        playersButton = transform.Find("Players Icon").gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (settingsMenu.activeSelf || playersMenu.activeSelf)
            {
                EnabledResumeMenu();
            }
        }
    }

    public void EnabledResumeMenu()
    {
        if (settingsMenu.activeSelf) settingsMenu.SetActive(false);
        if (playersMenu.activeSelf) playersMenu.SetActive(false);
        
        switch (resumeMenu)
        {
            case "dead":
                deadMenu.SetActive(true);
                break;
            case "pause":
                pauseMenu.SetActive(true);
                break;
            case "victory":
                victoryMenu.SetActive(true);
                break;
        }

        if (settingsMark.isOn) settingsMark.isOn = false;
        if (playersMark.isOn) playersMark.isOn = false;
        resumeMark.isOn = true;
    }

    public void EnabledSettingsMenu()
    {
        if (deadMenu.activeSelf) deadMenu.SetActive(false);
        if (pauseMenu.activeSelf) pauseMenu.SetActive(false);
        if (victoryMenu.activeSelf) victoryMenu.SetActive(false);
        if (playersMenu.activeSelf) playersMenu.SetActive(false);
        settingsMenu.SetActive(true);

        if (resumeMark.isOn) resumeMark.isOn = false;
        if (playersMark.isOn) playersMark.isOn = false;
        settingsMark.isOn = true;
    }

    public void EnabledPlayerMenu()
    {
        if (deadMenu.activeSelf) deadMenu.SetActive(false);
        if (pauseMenu.activeSelf) pauseMenu.SetActive(false);
        if (victoryMenu.activeSelf) victoryMenu.SetActive(false);
        if (settingsMenu.activeSelf) settingsMenu.SetActive(false);
        playersMenu.SetActive(true);

        if (resumeMark.isOn) resumeMark.isOn = false;
        if (settingsMark.isOn) settingsMark.isOn = false;
        playersMark.isOn = true;
    }

    public void Pause()
    {
        menu.SetActive(true);
        MenuEnabledEvent.Invoke();
        pauseMenu.SetActive(true);
        
        resumeMenu = "pause";
        resumeButton.sprite = resumeButtonOn;
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
            MenuEnabledEvent.Invoke();
        }
        deadMenu.SetActive(true);
        
        resumeMenu = "dead";
        resumeButton.sprite = resumeButtonOff;
    }

    public void Victory()
    {
        if (deadMenu.activeSelf)
        {
            deadMenu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
            MenuEnabledEvent.Invoke();
        }
        victoryMenu.SetActive(true);
        
        resumeMenu = "victory";
        resumeButton.sprite = resumeButtonOff;
    }
}
