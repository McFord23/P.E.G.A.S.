using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    Layer layerController;

    void Start()
    {
        layerController = GetComponentInParent<Layer>();
        SetTransparency();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            layerController.UploadChunks(transform.gameObject);
        }
    }

    void SetTransparency()
    {
        Material material;
        Color color;

        for (int i = 0; i < transform.childCount; i++)
        {
            material = transform.GetChild(i).gameObject.GetComponent<Renderer>().material;
            color = material.color;
            color.a = layerController.transparency;
            material.color = color;
        }
    }
}
