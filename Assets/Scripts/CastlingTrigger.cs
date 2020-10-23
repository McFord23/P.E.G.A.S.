using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlingTrigger : MonoBehaviour
{
    public Castling player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.ChangePrincess();
        }
    }
}
