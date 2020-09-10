using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidDinamic : MonoBehaviour
{
    Vector3 startPosition;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        startPosition = transform.position;
    }

    void OnTriggerEnter2D()
    {
        rb.gravityScale = 1f;
    }

    public void Reset()
    {
        rb.gravityScale = 0f;
        transform.position = startPosition;
    }
}
