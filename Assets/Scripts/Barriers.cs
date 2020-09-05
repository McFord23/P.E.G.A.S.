using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriers : MonoBehaviour
{
    //List<BarrierDinamic> listOfBarriers = new List<BarrierDinamic>();
    //BarrierDinamic[] barrier;

    /*void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
         {
            barrier[i] = transform.GetChild(i).gameObject.GetComponentInChildren<BarrierDinamic>();
            listOfBarriers.Add(barrier[i]);
         }
    }*/

    public void Reset()
    {
        /*for (int i = 0; i < transform.childCount; i++)
        {
            barrier[i].Reset();
        }*/

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponentInChildren<BarrierDinamic>().Reset();
        }
    }
}
