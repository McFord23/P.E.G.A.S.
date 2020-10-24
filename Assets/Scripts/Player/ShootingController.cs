using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject fireball;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(fireball, transform.position, transform.rotation);
        }
    }
}
