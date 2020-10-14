using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixerGroup mixer;
<<<<<<< HEAD
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
=======

    public void EnableSound()
    {
        Save.Sound = !Save.Sound;
>>>>>>> parent of c72a22f... Merge branch 'kpereb' into Pavel
        if (Save.Sound) mixer.audioMixer.SetFloat("SoundVolume", 0);
        else mixer.audioMixer.SetFloat("SoundVolume", -80);
    }

<<<<<<< HEAD

    public void EnableMusic(bool value)
    {
        Save.Music = value;
=======
    public void EnableMusic()
    {
        Save.Music = !Save.Music;
>>>>>>> parent of c72a22f... Merge branch 'kpereb' into Pavel
        if (Save.Music) mixer.audioMixer.SetFloat("MusicVolume", 0);
        else mixer.audioMixer.SetFloat("MusicVolume", -80);
    }

    public void ChangeVolume(Slider volume)
    {
<<<<<<< HEAD
        AudioListener.volume = volume.value;
=======
        Save.Volume = volume.value;
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, Save.Volume));
>>>>>>> parent of c72a22f... Merge branch 'kpereb' into Pavel
    }
}
