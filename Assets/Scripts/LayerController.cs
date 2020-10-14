using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    public float speed = 1f;
    public Player player;
    Vector3 position;
    Vector3 spawnPosition;
    Vector3 playerDirection;

    void Start()
    {
        spawnPosition = transform.position;
    }

    
   void Update()
    {
        playerDirection = player.transform.right;
        if (playerDirection.x > 0)
        {
            position = Vector3.right * speed * player.speed * Time.deltaTime;
            transform.position -= new Vector3(position.x, 0, 0);
        } 
        else
        {
            position = Vector3.right * speed * player.speed * Time.deltaTime;
            transform.position += new Vector3(position.x, 0, 0);
        }
    }

    public void Reset()
    {
        transform.position = spawnPosition;
    
    }

}
