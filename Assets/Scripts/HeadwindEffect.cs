using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadwindEffect : MonoBehaviour
{
    public Player player;
    float ratio;
    Vector3 offset;

    ParticleSystem headwind;
    ParticleSystem.MainModule main;
    ParticleSystem.EmissionModule emission;

    void Start()
    {
        headwind = GetComponent<ParticleSystem>();

        main = headwind.main;
        emission = headwind.emission;

        offset = transform.position - Camera.main.transform.position;
    }

    void Update()
    {
        ratio = player.speed / 150f;
        main.startSpeed = ratio * 100;
        emission.rateOverTime = ratio * 250;
        transform.position = new Vector3(Camera.main.transform.position.x + offset.x, transform.position.y, 0);
    }
}
