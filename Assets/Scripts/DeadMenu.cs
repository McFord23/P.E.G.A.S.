using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
    public Player player;
    public GameObject deadMenu;
    private Button fakeButton;

    public void Retry()
    {
        deadMenu.SetActive(false);

        player.Reset();
        fakeButton.Select();
    }

    public void Exit()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("Menu");
    }

    void Start()
    {
        fakeButton = deadMenu.GetComponent<Button>();
    }
}
