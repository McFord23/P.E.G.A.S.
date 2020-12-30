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
    MenuNavigation navigation;

    Toggle resumeMark;
    Toggle settingsMark;
    Toggle playersMark;

    Image resumeIcon;
    public Sprite resumeIconOn;
    public Sprite resumeIconOff;

    Image playersIcon;
    public Sprite soloIcon;
    public Sprite togetherIcon;

    public UnityEvent MenuEnabledEvent;
    public UnityEvent MenuDisabledEvent;

    bool isSelectedState = false;

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
        settingsMenu.SetActive(false);
        playersMenu.SetActive(false);
        menu.SetActive(false);
        MenuDisabledEvent.Invoke();

        navigation = GetComponent<MenuNavigation>();

        resumeMark = transform.Find("Resume Mark").GetComponent<Toggle>();
        settingsMark = transform.Find("Settings Mark").GetComponent<Toggle>();
        playersMark = transform.Find("Players Mark").GetComponent<Toggle>();

        resumeIcon = transform.Find("Resume Mark/Resume Icon").GetComponent<Image>();
        playersIcon = transform.Find("Players Mark/Players Icon").GetComponent<Image>();
    }

    void Update()
    {
        if (!isSelectedState && Input.GetButtonDown("Cancel"))
        {
            if (settingsMenu.activeSelf || playersMenu.activeSelf)
            {
                Back();
            }
        }
    }

    public void Back()
    {
        if (settingsMenu.activeSelf) settingsMenu.SetActive(false);
        if (playersMenu.activeSelf) playersMenu.SetActive(false);
        
        switch (resumeMenu)
        {
            case "dead":
                deadMenu.SetActive(true);
                navigation.StartSelect("Dead");
                break;
            case "pause":
                pauseMenu.SetActive(true);
                navigation.StartSelect("Pause");
                break;
            case "victory":
                victoryMenu.SetActive(true);
                navigation.StartSelect("Victory");
                break;
        }       

        if (settingsMark.isOn) settingsMark.isOn = false;
        if (playersMark.isOn) playersMark.isOn = false;
        resumeMark.isOn = true;
    }

    public void EnabledResumeMenu()
    {
        if (settingsMenu.activeSelf) settingsMenu.SetActive(false);
        if (playersMenu.activeSelf) playersMenu.SetActive(false);

        switch (resumeMenu)
        {
            case "dead":
                deadMenu.SetActive(true);
                navigation.SetMarkNavigation("Dead");
                break;
            case "pause":
                pauseMenu.SetActive(true);
                navigation.SetMarkNavigation("Pause");
                break;
            case "victory":
                victoryMenu.SetActive(true);
                navigation.SetMarkNavigation("Victory");
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
        navigation.SetMarkNavigation("Settings");
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
        navigation.SetMarkNavigation("Players");
    }

    public void UpdatePlayersIcon()
    {
        if (Save.TogetherMode) playersIcon.sprite = togetherIcon;
        else playersIcon.sprite = soloIcon;
    }

    public void Pause()
    {
        menu.SetActive(true);
        MenuEnabledEvent.Invoke();
        pauseMenu.SetActive(true);

        resumeMenu = "pause";
        resumeIcon.sprite = resumeIconOn;
        navigation.StartSelect("Pause");
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
        resumeIcon.sprite = resumeIconOff;
        navigation.StartSelect("Dead");
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
        resumeIcon.sprite = resumeIconOff;
        navigation.StartSelect("Victory");
    }

    public void SelectedState(bool var)
    {
        isSelectedState = var;
    }
}
