using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlingTrigger : MonoBehaviour
{
    public Castling player;
    public bool used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !used)
        {
            used = true;
            player.ChangePrincess();
        }
    }
}
