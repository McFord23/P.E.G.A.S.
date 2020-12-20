using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Cannon cannon;
    public GameObject barriers;
    Player player;
    DevOps devOps;
    GUIStyle style;

    void Start()
    {
        player = GetComponentInParent<Player>();
        devOps = GetComponent<DevOps>();
        style = new GUIStyle();
    }

    void Update()
    {
        style.normal.textColor = (devOps.pauseMenu.activeSelf || devOps.deadMenu.activeSelf || devOps.victoryMenu.activeSelf) ? Color.white : style.normal.textColor = Color.black;
    }

    void OnGUI()
    {
        GUILayout.Label("Developer Mode: " + devOps.mode, style);
        GUILayout.Label("Godness mode: " + player.godnessMode, style);
        GUILayout.Label("Barriers: " + barriers.activeSelf, style);
        GUILayout.Label("");
        GUILayout.Label("Cannon power: " + cannon.power, style);
        GUILayout.Label("");
        GUILayout.Label("MoveState: " + player.moveState, style);
        GUILayout.Label("Landed: " + player.landed, style);
        GUILayout.Label("");
        GUILayout.Label("Velocity: " + player.velocity, style);
        GUILayout.Label("Speed: " + (int)player.speed, style);
        GUILayout.Label("Angle: " + (int)player.angle, style);
        GUILayout.Label("Angle of Attack: " + (player.angleOfAttack - player.angleOfAttack % 0.01), style);
    }
}
