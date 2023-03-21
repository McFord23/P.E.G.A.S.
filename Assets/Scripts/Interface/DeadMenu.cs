using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DeadMenu : MonoBehaviour
{
    GameObject deadMenu;
    GameObject menu;
    UnityEvent MenuDisabledEvent;

    [FormerlySerializedAs("playersController")] 
    public PlayersManager playersManager;

    void Start()
    {
        deadMenu = transform.gameObject;
        menu = transform.parent.gameObject;
        MenuDisabledEvent = menu.GetComponent<MenuController>().MenuDisabledEvent;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            Retry();
        }
    }

    public void Retry()
    {
        playersManager.Reset();
        MenuDisabledEvent.Invoke();
        deadMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void Exit()
    {
        Save.players[0].live = true;
        Save.players[1].live = true;
        SceneManagerAdapter.LoadScene("Main Menu");
    }
}
