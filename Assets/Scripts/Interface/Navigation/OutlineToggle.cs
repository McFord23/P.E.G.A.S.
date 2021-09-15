using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineToggle : MonoBehaviour
{
    Toggle toggle;

    void Start()
    {
        toggle = GetComponentInParent<Toggle>();
    }

    public void Press()
    {
        toggle.isOn = !toggle.isOn;
    }
}
