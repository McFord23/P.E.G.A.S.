using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class BarrierDinamic : MonoBehaviour
{
    public bool isCycle;
    public float speed = 2.5f;
    public GameObject waitObject;
    bool move = false;
    bool paused = false;
    Vector3 startPosition;
    public Vector3 finishPosition;
    Vector3 target;

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

        if (move && !paused)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (waitObject != null)
        {
            if (collider.gameObject == waitObject)
            {
                move = true;
            }
        }
        else
        {
            if (collider.tag == "Player")
            {
                move = true;
            }
        }
        
    }

    public void Reset()
    {
        move = false;
        transform.localPosition = startPosition;
    }

    private void Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * speed);
    }

    public void Pause(bool check)
    {
        paused = check;
    }
}
