using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    new Camera camera;
    bool action = true;
    Vector3 offset;
    float size = 10f;

    private void Start()
    {
        camera = GetComponent<Camera>();
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        if (action)
        {
            transform.position = player.transform.position + offset + new Vector3(0, 0, -10);
            if (size < 10f) size += 0.1f;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 0, -10), Time.deltaTime * 3f);
            if (size > 5f) size -= 0.05f;
        }

        camera.orthographicSize = size;
    }

    public void FocusOnPlayer()
    {
        action = false;
    }

    public void FocusOnFly()
    {
        action = true;
    }
}