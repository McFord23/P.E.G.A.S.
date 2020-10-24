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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !used)
        {
            used = true;
            layerController.UploadChunks(transform.gameObject);
        }
    }
}
