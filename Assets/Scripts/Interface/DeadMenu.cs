using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
    Player player;

    public GameObject deadMenu;
    private Button fakeButton;

    public AudioSource clickSound;

    void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        fakeButton = deadMenu.GetComponent<Button>();
    }

    public void Retry()
    {
        clickSound.Play();
        
        if (deadMenu.activeSelf) fakeButton.Select();
        deadMenu.SetActive(false);

        player.Reset();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
