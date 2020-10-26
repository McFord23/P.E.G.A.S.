using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public bool isCycle;
    public float speed = 2.5f;
    public GameObject waitObject;
    bool move = false;
    bool freeze = false;
    public bool pause = false;
    Vector3 startPosition;
    public Vector3 finishPosition;
    Vector3 target;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (!isCycle || transform.localPosition == startPosition)
        {
            target = finishPosition;
        }
        else if (transform.localPosition == finishPosition)
        {
            target = startPosition;
        }

        if (move && !freeze && !pause)
        {
            Move();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (waitObject != null)
        {
            if (collider.gameObject == waitObject) move = true;
        }
        else
        {
            if (collider.gameObject.CompareTag("Player")) move = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) freeze = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) freeze = false;
    }

    public void Reset()
    {
        move = false;
        freeze = false;
        transform.localPosition = startPosition;
    }

    void Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
    }
}
