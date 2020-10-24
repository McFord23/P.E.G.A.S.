using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    Transform flap;
    AudioSource flapSound;
    AudioSource[] flapSounds;

    string scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene().name;

        switch (scene)
        {
            case "Game":
                flap = transform.Find("Flap");
                flapSounds = new AudioSource[flap.transform.childCount];
                for (int i = 0; i < flap.transform.childCount; i++)
                {
                    flapSounds[i] = flap.transform.GetChild(i).gameObject.GetComponentInChildren<AudioSource>();
                }
                break;
        }
    }

    public void Flap()
    {
        flapSound = flapSounds[Random.Range(0, transform.childCount)];
        flapSound.Play();
    }
}
