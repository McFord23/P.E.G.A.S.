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
        player = GameObject.Find("Player").GetComponent<Player>();
        fireball = transform.gameObject;
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(1,0) * (speed + player.rb.velocity.magnitude), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (!collider.gameObject.CompareTag("Fireball"))
        {
            Destroy(fireball);
        }
    }
}