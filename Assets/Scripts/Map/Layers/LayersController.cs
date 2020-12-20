using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersController : MonoBehaviour
{
    Layer[] layer;

    void Start()
    {
        layer = new Layer[transform.childCount];
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            layer[i] = transform.GetChild(i).gameObject.GetComponent<Layer>();
        }
    }

    public void Reset()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            layer[i].Reset();
        }
    }
}
