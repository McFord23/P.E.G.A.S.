using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
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
        style.normal.textColor = (devOps.pauseMenu.activeSelf || devOps.deadMenu.activeSelf || devOps.victoryMenu.activeSelf) ? Color.white : Color.black;
    }

    private void OnGUI()
    {
        GUILayout.Label("Flap - Space / LMB", style);
        GUILayout.Label("Retry - R", style);
        GUILayout.Label("Pause - Esc", style);
        GUILayout.Label("------------------------------", style);
        GUILayout.Label("Info controller - F1", style);
        GUILayout.Label("Debug screen - F2", style);
        GUILayout.Label("On/off DevMode - F3", style);
        GUILayout.Label("Godness mode - F4", style);
        GUILayout.Label("Teleport to next point - F5", style);
        GUILayout.Label("Set spawn on current point - F6", style);
    }
}
