using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixerGroup mixer;

    public void EnableSound()
    {
        Save.Sound = !Save.Sound;
        if (Save.Sound) mixer.audioMixer.SetFloat("SoundVolume", 0);
        else mixer.audioMixer.SetFloat("SoundVolume", -80);
    }

    public void EnableMusic()
    {
        Save.Music = !Save.Music;
        if (Save.Music) mixer.audioMixer.SetFloat("MusicVolume", 0);
        else mixer.audioMixer.SetFloat("MusicVolume", -80);
    }

    public void ChangeVolume(Slider volume)
    {
        Save.Volume = volume.value;
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, Save.Volume));
    }
}
