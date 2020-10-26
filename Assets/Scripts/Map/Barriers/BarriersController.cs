using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriersController : MonoBehaviour
{
    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponentInChildren<Barrier>().Reset();
        }
    }

    public void Pause()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponentInChildren<Barrier>().pause = true;
        }
    }

    public void Resume()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponentInChildren<Barrier>().pause = false;
        }
    }
}
