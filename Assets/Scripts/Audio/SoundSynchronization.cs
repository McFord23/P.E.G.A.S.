using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSynchronization : MonoBehaviour
{
    public GameObject checkmark;

    void Update()
    {
        checkmark.SetActive(Save.Sound);
    }
}
