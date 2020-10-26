using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeRainTrigger : MonoBehaviour
{
    CupcakeRain rainController;
    private void Start()
    {
        rainController = GetComponent<CupcakeRain>();
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
