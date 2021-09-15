using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindigosDestructor : MonoBehaviour
{
    public new CameraController camera;

    void Start()
    {
        transform.position = camera.transform.position;
    }

    void Update()
    {
        transform.position = camera.transform.position;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Windigo"))
        {
            collider.gameObject.GetComponent<Windigo>().Destroy();
        }
    }
}
