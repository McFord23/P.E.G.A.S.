using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineSet : MonoBehaviour
{
    Button button;
    Button set;
    Button nextSet;
    Button perviousSet;

    bool isSetSelected = false;

    void Start()
    {
        button = GetComponent<Button>();
        set = transform.Find("Outline").GetComponent<Button>();
        nextSet = transform.Find("Next Set").GetComponent<Button>();
        perviousSet = transform.Find("Pervious Set").GetComponent<Button>();
    }

    void Update()
    {
        if (isSetSelected)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                button.Select();
                isSetSelected = false;
            }

            if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0)
            {
                nextSet.onClick.Invoke();
            }
            else if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0)
            {
                perviousSet.onClick.Invoke();
            }
        }
    }

    public void Press()
    {
        set.Select();
        isSetSelected = true;
    }
}
