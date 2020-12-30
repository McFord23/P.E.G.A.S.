using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Slider mouseSlider;
    Slider keyboardSlider;

    void Start()
    {
        mouseSlider = transform.Find("Mouse Slider").gameObject.GetComponent<Slider>();
        keyboardSlider = transform.Find("Keyboard Slider").gameObject.GetComponent<Slider>();

        mouseSlider.value = Save.Sensitivity.mouse;
        keyboardSlider.value = Save.Sensitivity.keyboard;
    }

    public void SetMouseSensitivity(float value)
    {
        Save.Sensitivity.mouse = value;
    }

    public void SetKeyboardSensitivity(float value)
    {
        Save.Sensitivity.keyboard = value;
    }
}
