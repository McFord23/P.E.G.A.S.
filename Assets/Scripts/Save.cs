using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Save
{
    static string princessName = "Celestia";
    static bool soundEnable = true;
    static bool musicEnable = true;
    static float masterVolume = 1;

    public static string Princess
    {
        get
        {
            return princessName;
        }
        set
        {
            princessName = value;
        }
    }

    public static bool Sound
    {
        get
        {
            return soundEnable;
        }
        set
        {
            soundEnable = value;
        }
    }

    public static bool Music
    {
        get
        {
            return musicEnable;
        }
        set
        {
            musicEnable = value;
        }
    }

    public static float Volume
    {
        get
        {
            return masterVolume;
        }
        set
        {
            masterVolume = value;
        }
    }
}
