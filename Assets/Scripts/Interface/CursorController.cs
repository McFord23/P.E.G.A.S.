using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CursorUnlock()
    {
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
