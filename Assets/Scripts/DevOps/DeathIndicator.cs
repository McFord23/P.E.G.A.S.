using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathIndicator : MonoBehaviour
{
    GUIStyle style;
    Player player;
    Vector2 playerPos;
 
    void Start()
    {
        style = new GUIStyle() 
        { 
            normal = new GUIStyleState() 
            { 
                textColor = Color.red 
            }, 
            fontSize = 24 
        };

        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        playerPos = Camera.main.WorldToScreenPoint(player.transform.position);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(playerPos.x, playerPos.y, 100, 100), "BOOM", style);
    }
}
