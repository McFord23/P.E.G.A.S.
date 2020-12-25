using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject fireball;
    public Transform parent;
    Player player;
    float shootInput;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void FixedUpdate()
    {
        shootInput = GetComponentInParent<PlayerController>().GetShootInput();

        if (player.moveState != Player.MoveState.Paused && player.moveState != Player.MoveState.Dead && player.moveState != Player.MoveState.Winner)
        {
            if (shootInput > 0)
            {
                Instantiate(fireball, transform.position, transform.rotation, parent);
            }
        }
    }
}
