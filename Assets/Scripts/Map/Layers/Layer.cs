using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public float speed = 1f;
    public float transparency = 1f;
    public GameObject chunk;
    public float size = 111.86f;
    Vector3[] spawnPositions;
    Vector3 position;

    void Start()
    {
        spawnPositions = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPositions[i] = transform.GetChild(i).gameObject.transform.position;
        }
    }

    
   void FixedUpdate()
    {
        position = Vector3.right * speed * Time.deltaTime;
        transform.position -= position;
    }

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.transform.position = spawnPositions[i];
        }
    }

    public void UploadChunks(GameObject chunkCurrent)
    {
        Instantiate(chunk, chunkCurrent.transform.position + new Vector3(2 * size, 0f, 0f), chunkCurrent.transform.rotation, transform);
        Destroy(chunkCurrent);
    }
}
