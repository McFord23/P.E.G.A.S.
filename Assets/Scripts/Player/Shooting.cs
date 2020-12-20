using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject fireball;
    Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        if (player.moveState == Player.MoveState.Idle || player.moveState == Player.MoveState.Run || player.moveState == Player.MoveState.FreeFall || player.moveState == Player.MoveState.Flap)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                Instantiate(fireball, transform.position, transform.rotation);
            }
        }
    }
}
