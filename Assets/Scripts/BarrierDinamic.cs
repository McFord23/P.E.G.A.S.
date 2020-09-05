using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDinamic : MonoBehaviour
{
    public bool isCycle;
    private Vector3 startPosition;
    public Vector3 finishPosition;
    private Vector3 target;

    public void Reset()
    {
        transform.localPosition = startPosition;
    }

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

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, Time.deltaTime * 2.5f);
    }
}
