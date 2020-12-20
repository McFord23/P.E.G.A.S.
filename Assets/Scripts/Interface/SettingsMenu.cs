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
        mouseSlider = transform.Find("MouseSlider").gameObject.GetComponent<Slider>();
        keyboardSlider = transform.Find("KeyboardSlider").gameObject.GetComponent<Slider>();

        mouseSlider.value = Save.MouseSensitivity;
        keyboardSlider.value = Save.KeyboardSensitivity;
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
