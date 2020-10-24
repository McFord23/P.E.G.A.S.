using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    LayerController layerController;
    bool used = false;

    void Start()
    {
        layerController = GetComponentInParent<LayerController>();
        SetTransparency();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !used)
        {
            used = true;
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
