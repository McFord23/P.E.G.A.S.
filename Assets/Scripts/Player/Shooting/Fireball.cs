using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Player player;

    GameObject fireball;
    Rigidbody2D rb;
    public float speed;

    void Start()
    {
        player = GetComponentInParent<FireballsController>().player;
        
        fireball = transform.gameObject;
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(1, 0) * (speed + player.speed), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Fireball"))
        {
            Destroy(fireball);
        }
    }
}