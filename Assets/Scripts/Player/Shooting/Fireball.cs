using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    GameObject fireball;
    Rigidbody2D rb;
    public float speed;
    float playerSpeed;

    void Start()
    {
        playerSpeed = GetComponentInParent<FireballsController>().playersController.GetPlayerSpeed();
        
        fireball = transform.gameObject;
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(1, 0) * (speed + playerSpeed), ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Fireball") && !collider.gameObject.CompareTag("Layer"))
        {
            Destroy(fireball);
        }
    }
}