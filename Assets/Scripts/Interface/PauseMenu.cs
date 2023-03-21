using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenu;
    GameObject menu;
    UnityEvent MenuDisabledEvent;

    [FormerlySerializedAs("playersController")] 
    public PlayersManager playersManager;

    void Start()
    {
        pauseMenu = transform.gameObject;
        menu = transform.parent.gameObject;
        MenuDisabledEvent = menu.GetComponent<MenuController>().MenuDisabledEvent;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Resume();
        }
    }

    public void Resume()
    {
        playersManager.Resume();
        MenuDisabledEvent.Invoke();
        pauseMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void Exit()
    {
        Save.players[0].live = true;
        Save.players[1].live = true;
        SceneManagerAdapter.LoadScene("Main Menu");
    }
}
