using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    public Camera cam;
    public bool isActive = false;
    GameObject[] CapCakes;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CapCakes = new GameObject[transform.childCount];
        rb.gravityScale = 0f;

    }
    void Update()
    {
        if (!isActive)
        {
            transform.position = cam.transform.position + new Vector3(0, 50, 10);
        }
    }

    public void Reset()
    {
        isActive = false;
        rb.gravityScale = 0f;
        transform.position = cam.transform.position + new Vector3(0, 50, 10);    
    }

    public void Spawn()
    {
        Randomizer();
        isActive = true;
        rb.gravityScale = 0.5f;
    }

    private void Randomizer()
    {
        int enable;
        for (int i = 0; i < transform.childCount; i++)
        {
            enable = Random.Range(0, 2);
            if (enable == 0)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            } 
            else 
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame

}
