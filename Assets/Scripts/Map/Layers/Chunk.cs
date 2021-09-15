using UnityEngine;

public class Chunk : MonoBehaviour
{
    Layer layerController;

    void Start()
    {
        layerController = GetComponentInParent<Layer>();
        SetTransparency();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            var direction = collider.transform.parent.GetComponentInParent<PlayersController>().GetPlayerDirection();
            layerController.UploadChunks(transform.gameObject, direction);
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
