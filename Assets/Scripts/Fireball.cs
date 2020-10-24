using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    GameObject fireball;
    Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(1,0) * speed,ForceMode2D.Force);
    }

    void onCollisionEnter2D()
    {
        Destroy(fireball);
    }
}