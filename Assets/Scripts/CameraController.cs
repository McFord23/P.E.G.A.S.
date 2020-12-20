using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;
    int mode = 1;
    Vector3 offset;
    Vector3 spawnPos;

    public float maxSize = 10f;
    public float minSize = 5f;
    public float size;

    void Start()
    {
        spawnPos = transform.position;
        offset = transform.position - player.transform.position;
        size = maxSize;
    }

    void FixedUpdate()
    {
        switch (mode)
        {
            case 0:
                transform.position = spawnPos;
                if (size < maxSize) size += 0.5f;
                break;
            case 1:
                transform.position = player.transform.position + offset + new Vector3(0, 0, -10);
                if (size < maxSize) size += 0.5f;
                break;
            case 2:
                transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 0, -10), Time.deltaTime * 3f);
                if (size > minSize) size -= 0.1f;
                break;
        }

        Camera.main.orthographicSize = size;
    }

    public void FocusOnCannon()
    {
        mode = 0;
    }

    public void FocusOnFly()
    {
        mode = 1;
    }

    public void FocusOnPlayer()
    {
        mode = 2;
    }
}