using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject fireball;
    public Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && (player.moveState != Player.MoveState.Dead) && (player.moveState != Player.MoveState.Paused))
        {
            Instantiate(fireball, transform.position, transform.rotation);
        }
    }
}
