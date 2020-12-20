using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkNavigation : MonoBehaviour
{
    public Toggle[] bookmarks;

    int index = 0;

    void Update()
    {
        if (Input.GetAxis("Rotate") > 0)
        {
            if (index < bookmarks.Length)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }

        if (Input.GetAxis("Rotate") < 0)
        {
            if (index > 0)
            {
                index--;
                bookmarks[index].Select();
            }
            else
            {
                index = 0;
            }
        }
    }
}
