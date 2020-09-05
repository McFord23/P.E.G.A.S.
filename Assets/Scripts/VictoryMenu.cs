using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    public new CameraFollow camera;
    public GameObject victoryMenu;

    public void Continued()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (victoryMenu.activeInHierarchy)
        {
            camera.FocusOnPlayer();
        }
    }
}
