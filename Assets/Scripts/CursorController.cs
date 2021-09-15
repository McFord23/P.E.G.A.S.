using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    bool isMenuActive = false;
    Vector3 oldMousePos;

    void Start()
    {
        oldMousePos = Input.mousePosition;
    }

    void Update()
    {
        if (isMenuActive)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.mousePosition != oldMousePos && Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            oldMousePos = Input.mousePosition;
        }
    }

    public void StartMonitoring()
    {
        isMenuActive = true;
    }

    public void StopMonitoring()
    {
        isMenuActive = false;
    }

    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CursorUnlock()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CursorVisible()
    {
        Cursor.visible = true;
    }

    public void CursorUnvisible()
    {
        Cursor.visible = false;
    }
}
