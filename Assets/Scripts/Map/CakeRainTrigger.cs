using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeRainTrigger : MonoBehaviour
{
    RainController rainController;
    private void Start()
    {
        rainController = GetComponent<RainController>();
    }
 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rainController.Active(true);
        }
    } 

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rainController.Active(false);
        }
    }
}
