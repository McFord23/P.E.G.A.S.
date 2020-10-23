using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioSource flyingMusic;
    public AudioSource pauseMusic;
    public AudioSource menuMusic;
    public AudioSource victoryMusic;
    public AudioSource creditsMusic;

    AudioSource celestiaMenuStart;
    AudioSource celestiaMenuLoop;
    AudioSource lunaMenu;

    AudioSource lunaFlying;
    AudioSource celestiaFlyingStart;
    AudioSource celestiaFlyingLoop;

    AudioSource lunaPause;
    AudioSource celestiaPause;

    string scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene().name;

        switch (scene)
        {
            case "Menu":
                celestiaMenuStart = transform.Find("Celestia Menu (start)").GetComponentInChildren<AudioSource>();
                celestiaMenuLoop = transform.Find("Celestia Menu (loop)").GetComponentInChildren<AudioSource>();
                lunaMenu = transform.Find("Luna Menu").GetComponentInChildren<AudioSource>();

                switch (Save.Princess)
                {
                    case "Luna":
                        menuMusic = lunaMenu;
                        break;

                    case "Celestia":
                        menuMusic = celestiaMenuStart;
                        break;
                }

                menuMusic.Play();
                break;

            case "Game":
                lunaFlying = transform.Find("Luna Flying").GetComponentInChildren<AudioSource>();
                lunaPause = transform.Find("Luna Pause").GetComponentInChildren<AudioSource>();
                celestiaFlyingStart = transform.Find("Celestia Flying (start)").GetComponentInChildren<AudioSource>();
                celestiaFlyingLoop = transform.Find("Celestia Flying (loop)").GetComponentInChildren<AudioSource>();
                celestiaPause = transform.Find("Celestia Pause").GetComponentInChildren<AudioSource>();
                victoryMusic = transform.Find("Victory").GetComponentInChildren<AudioSource>();

                switch (Save.Princess)
                {
                    case "Luna":
                        flyingMusic = lunaFlying;
                        pauseMusic = lunaPause;
                        break;

                    case "Celestia":
                        flyingMusic = celestiaFlyingStart;
                        pauseMusic = celestiaPause;
                        break;
                }

                flyingMusic.Play();
                break;

            case "Credits":
                creditsMusic.Play();
                break;
        }
    }

    private void Update()
    {
        switch (scene)
        {
            case "Menu":
                if (Save.Princess == "Celestia" && !menuMusic.isPlaying)
                {
                    menuMusic.Stop();
                    menuMusic = celestiaMenuLoop;
                    menuMusic.Play();
                }
                break;

            case "Game":
                if (Save.Princess == "Celestia" && !(flyingMusic.loop) && (flyingMusic.time >= 116.5f))
                {
                    flyingMusic.Stop();
                    flyingMusic = celestiaFlyingLoop;
                    flyingMusic.Play();
                }
                break;
        }
    }

    public void ChangeTheme(string theme)
    {
        flyingMusic.Stop();
        
        switch (theme)
        {
            case "Luna":
                flyingMusic = lunaFlying;
                pauseMusic = lunaPause;
                break;

            case "Celestia":
                flyingMusic = celestiaFlyingStart;
                pauseMusic = celestiaPause;
                break;
        }

        flyingMusic.Play();
    }
}
