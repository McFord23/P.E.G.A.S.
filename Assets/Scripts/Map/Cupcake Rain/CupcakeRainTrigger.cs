using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeRainTrigger : MonoBehaviour
{
    CupcakeRain rainController;

    void Start()
    {
        rainController = GetComponent<CupcakeRain>();
    }
 
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            rainController.Active(true);
        }
    } 

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            rainController.Active(false);
        }
    }
}
