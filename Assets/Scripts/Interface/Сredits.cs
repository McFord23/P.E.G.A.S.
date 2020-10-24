using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Сredits : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(Exit), 42.17f);
    }

    void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
