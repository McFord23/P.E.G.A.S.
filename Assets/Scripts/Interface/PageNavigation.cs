using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageNavigation : MonoBehaviour
{
    public Button[] buttons;
    public Toggle[] settings;

    int index = 0;
    int indexY = 0;

    void Start()
    {
        buttons[index].Select();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (index < buttons.Length)
            {
                index++;
                buttons[index].Select();
            }
            else
            {
                settings[indexY].Select();
                index = 0;
            }
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            if (index > 0)
            {
                index--;
                buttons[index].Select();
            }
            else
            {
                settings[indexY].Select();
                index = 0;
            }
        }
    }
}
