using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadwindEffect : MonoBehaviour
{
    Player player;
    float ratio;
    Vector3 offset;

    ParticleSystem headwind;
    ParticleSystem.MainModule main;
    ParticleSystem.EmissionModule emission;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        headwind = GetComponent<ParticleSystem>();

        main = headwind.main;
        emission = headwind.emission;

        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        ratio = player.speed / 40f;

        main.startSpeed = ratio * 100;
        emission.rateOverTime = ratio * 100;

        transform.position = player.transform.position + offset;
    }
}
