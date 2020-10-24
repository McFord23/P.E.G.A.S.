using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffects : MonoBehaviour
{
    Player player;
    ParticleSystem lines;
    ParticleSystem.ColorOverLifetimeModule color;
    Gradient gradient = new Gradient();
    float ratio;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        lines = GetComponent<ParticleSystem>();
        color = lines.colorOverLifetime;
        gradient = new Gradient();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.white;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].time = 0.5f;
        alphaKey[1].time = 1.0f;
    }

    void Update()
    {
        alphaKey[0].alpha = player.speed / 40f;
        alphaKey[1].alpha = player.speed / 40f;
        gradient.SetKeys(colorKey, alphaKey);
        color.color = gradient;

        transform.position = player.transform.position;
    }
}
