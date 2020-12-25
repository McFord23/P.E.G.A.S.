using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;
    public GameObject celestia;
    public GameObject luna;

    public string mode = "fly-Celestia";
    public Vector3 offset;
    Vector3 spawnPos;

    public float maxSize = 10f;
    public float minSize = 5f;
    public float size;
    public float playersDistance;

    void Start()
    {
        spawnPos = transform.position;
        offset = new Vector3(5, 0, -10);
        size = maxSize;
    }

    void FixedUpdate()
    {
        switch (mode)
        {
            case "start":
                transform.position = spawnPos;
                if (size < maxSize) size += 0.5f;
                break;
            case "fly-Celestia":
                transform.position = celestia.transform.position + offset;
                if (size < maxSize) size += 0.5f;
                break;
            case "fly-Luna":
                transform.position = luna.transform.position + offset;
                if (size < maxSize) size += 0.5f;
                break;
            case "fly-together":
                var playersVector = celestia.transform.position - luna.transform.position;
                playersDistance = playersVector.magnitude;

                transform.position = (celestia.transform.position + luna.transform.position) / 2;
                if (size < playersDistance) size += 0.5f;
                break;
            case "Celestia":
                transform.position = Vector3.Lerp(transform.position, celestia.transform.position + new Vector3(0, 0, -10), Time.deltaTime * 3f);
                if (size > minSize) size -= 0.1f;
                break;
            case "Luna":
                transform.position = Vector3.Lerp(transform.position,luna.transform.position + new Vector3(0, 0, -10), Time.deltaTime * 3f);
                if (size > minSize) size -= 0.1f;
                break;
        }

        Camera.main.orthographicSize = size;
    }

    public void FocusOnStart()
    {
        mode = "start";
    }

    public void FocusOnFly(string character)
    {
        mode = "fly-" + character;
    }

    public void FocusOnPlayer(string character)
    {
        mode = character;
    }
}