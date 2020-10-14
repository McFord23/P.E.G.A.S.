﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixerGroup mixer;
<<<<<<< HEAD

    public void EnableSound()
    {
        Save.Sound = !Save.Sound;
=======
    public Slider volume;
    public Toggle soundToggle;
    public Toggle musicToggle;

    void Start()
    {
        volume.value = AudioListener.volume;
        soundToggle.isOn = Save.Sound;
        musicToggle.isOn = Save.Music;
    }

    public void EnableSound(bool value)
    {
        Save.Sound = value;
>>>>>>> kpereb
        if (Save.Sound) mixer.audioMixer.SetFloat("SoundVolume", 0);
        else mixer.audioMixer.SetFloat("SoundVolume", -80);
    }

<<<<<<< HEAD
    public void EnableMusic()
    {
        Save.Music = !Save.Music;
=======
    public void EnableMusic(bool value)
    {
        Save.Music = value;
>>>>>>> kpereb
        if (Save.Music) mixer.audioMixer.SetFloat("MusicVolume", 0);
        else mixer.audioMixer.SetFloat("MusicVolume", -80);
    }

    public void ChangeVolume(Slider volume)
    {
<<<<<<< HEAD
        Save.Volume = volume.value;
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, Save.Volume));
=======
        //Save.Volume = volume.value;
        //mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, Save.Volume));
        AudioListener.volume = volume.value;
>>>>>>> kpereb
    }
}
