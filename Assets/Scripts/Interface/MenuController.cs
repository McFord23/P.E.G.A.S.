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

    public Button deadStartButton;
    public Button pauseStartButton;
    public Button victoryStartButton;
    public Toggle settingsStartToggle;
    public Slider settingsMarkStartSlider;
    public Button playersStartButton;

    string resumeMenu;

    Toggle resumeMark;
    Toggle settingsMark;
    Toggle playersMark;

    Button[] markButtons = new Button[3];

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

        markButtons[0] = transform.Find("Resume Icon").gameObject.GetComponent<Button>();
        markButtons[1] = transform.Find("Settings Icon").gameObject.GetComponent<Button>();
        markButtons[2] = transform.Find("Players Icon").gameObject.GetComponent<Button>();

        resumeButton = markButtons[0].GetComponent<Image>();
        playersButton = markButtons[2].gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            if (settingsMenu.activeSelf || playersMenu.activeSelf)
            {
                EnabledResumeMenu();
            }
        }
    }

    public void SetMarkNavigation(Button startButton)
    {
        startButton.Select();
        foreach (Button button in markButtons)
        {
            Navigation navigation = button.navigation;
            navigation.selectOnLeft = startButton;
            button.navigation = navigation;
        }
    }

    public void SetMarkNavigation(Slider startSlider)
    {
        foreach (Button button in markButtons)
        {
            Navigation navigation = button.navigation;
            navigation.selectOnLeft = startSlider;
            button.navigation = navigation;
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
                SetMarkNavigation(deadStartButton);
                break;
            case "pause":
                pauseMenu.SetActive(true);
                SetMarkNavigation(pauseStartButton);
                break;
            case "victory":
                victoryMenu.SetActive(true);
                SetMarkNavigation(victoryStartButton);
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

        settingsMarkStartSlider.Select();
        SetMarkNavigation(settingsMarkStartSlider);
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

        playersStartButton.Select();
        SetMarkNavigation(playersStartButton);
    }

    public void Pause()
    {
        menu.SetActive(true);
        MenuEnabledEvent.Invoke();
        pauseMenu.SetActive(true);

        SetMarkNavigation(pauseStartButton);

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

        SetMarkNavigation(deadStartButton);

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

        SetMarkNavigation(victoryStartButton);

        resumeMenu = "victory";
        resumeButton.sprite = resumeButtonOff;
    }
}
