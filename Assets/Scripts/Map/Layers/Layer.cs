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

    public void UploadChunks(GameObject chunk, float direction)
    {
        if (chunk.transform.position == transform.GetChild(0).transform.position)
        {
            transform.GetChild(1).transform.position += new Vector3(size * direction, 0f, 0f);
        }
        else
        {
            transform.GetChild(0).transform.position += new Vector3(size * direction, 0f, 0f);
        }
    }
}
