using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public GameObject fireball;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(fireball,transform.position,transform.rotation);
        }
    }
}
