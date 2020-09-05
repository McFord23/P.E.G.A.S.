using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class BarrierDinamic : MonoBehaviour
{
    public bool isCycle;
    public float speed = 2.5f;
    private bool move = false;
    private Vector3 startPosition;
    public Vector3 finishPosition;
    private Vector3 target;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        if (!isCycle || transform.localPosition == startPosition)
        {
            target = finishPosition;
        }
        else if (transform.localPosition == finishPosition)
        {
            target = startPosition;
        }

        if (move)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            move = true;
        }
    }

    public void Reset()
    {
        move = false;
        transform.localPosition = startPosition;
    }
}
