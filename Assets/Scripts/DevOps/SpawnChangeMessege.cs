using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChangeMessege : MonoBehaviour
{
    DevOps devOps;
    GUIStyle style;

    void Start()
    {
        devOps = GetComponent<DevOps>();
        style = new GUIStyle();
    }

    void Update()
    {
        style.normal.textColor = (devOps.pauseMenu.activeSelf || devOps.deadMenu.activeSelf || devOps.victoryMenu.activeSelf) ? Color.white : style.normal.textColor = Color.black;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 150, 0, 100, 100), "Spawn Point Changed", style);
    }
}
