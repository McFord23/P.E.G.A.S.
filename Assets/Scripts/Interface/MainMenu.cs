using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSound;
    GameObject settingsMenu;
    public Toggle gear;
    public Slider mouseSlider;
    public Slider keyboardSlider;
    

    void Start()
    {
        settingsMenu = transform.Find("SettingsMenu").gameObject;
        mouseSlider.value = Save.MouseSensitivity;
        keyboardSlider.value = Save.KeyboardSensitivity;
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
        clickSound.Play();
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void EnabledSettings(bool value)
    {
        settingsMenu.SetActive(value);
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        gear.isOn = false;
    }

    public void SetMouseSensitivity(float value)
    {
        Save.MouseSensitivity = value;
    }

    public void SetKeyboardSensitivity(float value)
    {
        Save.KeyboardSensitivity = value;
    }
}
