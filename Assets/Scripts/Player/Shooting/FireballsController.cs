using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballsController : MonoBehaviour
{
    public Rigidbody2D GetFireball()
    {
        return transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>();
    }
}
