using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject fireball;
    Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && (player.moveState != Player.MoveState.Dead) && (player.moveState != Player.MoveState.Paused) && (player.moveState != Player.MoveState.Loaded))
        {
            Instantiate(fireball, transform.position, transform.rotation);
        }
    }
}
