using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayersController playersController;

    string mode = "fly";
    Vector3 offset;

    public float maxSize = 10f;
    public float minSize = 5f;
    float size;

    void Start()
    {
        offset = new Vector3(5, 0, -10);
        size = maxSize;
    }

    void FixedUpdate()
    {
        switch (mode)
        {
            case "fly":
                if (Save.TogetherMode)
                {
                    transform.position = playersController.GetPlayerPosition() + new Vector3(0, 0, -10);
                    var distance = playersController.GetDistanceBetweenPlayers();
                    if (distance >= maxSize) size = distance;
                }
                else
                {
                    transform.position = playersController.GetPlayerPosition() + offset;
                    if (size < maxSize) size += 0.1f;
                }
                break;
            case "player":
                transform.position = Vector3.Lerp(transform.position, playersController.GetPlayerPosition() + new Vector3(0, 0, -10), Time.deltaTime * 3f);
                if (!Save.TogetherMode)
                {
                    if (size > minSize) size -= 0.1f;
                } 
                break;
        }

        Camera.main.orthographicSize = size;
    }

    public void FocusOnFly()
    {
        mode = "fly";
    }

    public void FocusOnPlayer()
    {
        mode = "player";
    }
}