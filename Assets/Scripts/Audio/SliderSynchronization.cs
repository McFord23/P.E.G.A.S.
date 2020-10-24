using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSynchronization : MonoBehaviour
{
    Slider volume;

    void Start()
    {
        volume = GetComponent<Slider>();
    }

    void Update()
    {
        volume.value = Save.Volume;
    }
}
