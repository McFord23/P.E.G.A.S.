using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeRainTrigger : MonoBehaviour
{
    public RainController rainController;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rainController.Spawn();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rain"))
        {
            rainController.Reset();
        }
    }
}
