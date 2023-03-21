using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed = 1f;
    public float transparency = 1f;
    public float size = 111.86f;

    private Transform currentChunk;
    private Vector3 spawnPosCC;

    private Transform nextChunk;
    private Vector3 spawnPosNC;

    public void Initialize()
    {
        currentChunk = transform.GetChild(0);
        spawnPosCC = currentChunk.transform.position;

        nextChunk = transform.GetChild(1);
        spawnPosNC = nextChunk.transform.position;
    }
    
   private void FixedUpdate()
   {
        transform.Translate(Vector3.left * speed);
   }

    public void Reset()
    {
        currentChunk.position = spawnPosCC;
        nextChunk.position = spawnPosNC;
    }

    public void LoadChunk(Transform chunk, bool playerOnRight)
    {
        if (currentChunk != chunk)
        {
            nextChunk = currentChunk;
            currentChunk = chunk;

            SwapSpawnPoint();
        }

        if (playerOnRight) nextChunk.position = currentChunk.position - new Vector3(size, 0f, 0f);
        else nextChunk.position = currentChunk.position + new Vector3(size, 0f, 0f);
    }

    private void SwapSpawnPoint()
    {
        Vector3 temp = spawnPosCC;
        spawnPosCC = spawnPosNC;
        spawnPosNC = temp;
    }
}
