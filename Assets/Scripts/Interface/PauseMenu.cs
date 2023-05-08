using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    GameObject menu;
    UnityEvent MenuDisabledEvent;

    [FormerlySerializedAs("playersController")] 
    public PlayersManager playersManager;

    void Start()
    {
        menu = transform.parent.parent.gameObject;
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
        gameObject.SetActive(false);
        menu.SetActive(false);
    }

    public void Exit()
    {
        Global.players[0].live = true;
        Global.players[1].live = true;
        SceneManagerAdapter.Instance.LoadScene("Main Menu");
    }
}
