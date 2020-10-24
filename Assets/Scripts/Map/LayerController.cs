using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    public float speed = 1f;
    public float transparency = 1f;
    public GameObject chunk;
    Vector3[] spawnPositions;

    Player player;
    Vector3 position;
    Vector3 playerOldPosition;

    void Start()
    {
        spawnPositions = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPositions[i] = transform.GetChild(i).gameObject.transform.position;
        }

        player = GameObject.Find("Player").GetComponent<Player>();
        playerOldPosition = player.transform.position;

        //SetTransparency(transparency);
    }

    
   void Update()
    {
        position = Vector3.right * speed * player.speed * Time.deltaTime;
        if (playerOldPosition.x < player.transform.position.x)
        {
            transform.position -= new Vector3(position.x, 0, 0);
        } 
        else if (playerOldPosition.x > player.transform.position.x)
        {
            transform.position += new Vector3(position.x, 0, 0);
        }
        playerOldPosition.x = player.transform.position.x;

        if (player.moveState == Player.MoveState.Loaded)
        {
            Reset();
        }
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
        Instantiate(chunk, chunkCurrent.transform.position + new Vector3(335.58f, 0f, 0f), chunkCurrent.transform.rotation, transform);
        Destroy(chunkCurrent);
    }

    void SetTransparency(float alpha)
    {
        Material material;
        Color color;

        for (int i = 0; i < transform.childCount; i++)
        {
            material = transform.GetChild(i).gameObject.GetComponent<Renderer>().material;
            color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }

}
