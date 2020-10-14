using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixerGroup mixer;
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
        if (Save.Sound) mixer.audioMixer.SetFloat("SoundVolume", 0);
        else mixer.audioMixer.SetFloat("SoundVolume", -80);
    }


    public void EnableMusic(bool value)
    {
        Save.Music = value;
        if (Save.Music) mixer.audioMixer.SetFloat("MusicVolume", 0);
        else mixer.audioMixer.SetFloat("MusicVolume", -80);
    }

    public void ChangeVolume(Slider volume)
    {
        AudioListener.volume = volume.value;
    }
}
