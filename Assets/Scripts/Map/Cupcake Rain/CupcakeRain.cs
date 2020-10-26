using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CupcakeRain : MonoBehaviour
{
    ParticleSystem[] particleSystems;
    ParticleSystem.EmissionModule[] emissions;

    void Start()
    {
        particleSystems = new ParticleSystem[transform.childCount];
        emissions = new ParticleSystem.EmissionModule[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            particleSystems[i] = transform.GetChild(i).gameObject.GetComponent<ParticleSystem>();
            emissions[i] = particleSystems[i].emission; 
        }

        Active(false);
    }

    public void Active(bool val)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            emissions[i].enabled = val;
        }
    }

}
