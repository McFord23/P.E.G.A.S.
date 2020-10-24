using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    public float speed = 1f;
    public float transparency = 1f;
    public GameObject chunk;
    public float size = 111.86f;
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
    }

    
   void Update()
    {
        position = Vector3.right * speed * player.speed * Time.deltaTime;
        if (playerOldPosition.x < player.transform.position.x)
        {
            transform.position -= position;
        } 
        else if (playerOldPosition.x > player.transform.position.x)
        {
            transform.position += position;
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
        Instantiate(chunk, chunkCurrent.transform.position + new Vector3(2 * size, 0f, 0f), chunkCurrent.transform.rotation, transform);
        Destroy(chunkCurrent);
    }
}
