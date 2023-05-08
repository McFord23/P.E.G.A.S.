using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    private GameObject menu;
    private UnityEvent MenuDisabledEvent;

    [FormerlySerializedAs("playersController")] 
    [SerializeField] private PlayersManager playersManager;

    private void Start()
    {
        menu = transform.parent.parent.gameObject;
        MenuDisabledEvent = menu.GetComponent<MenuController>().MenuDisabledEvent;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ExecuteResume();
        }
    }

    public void ExecuteResume()
    {
        EventAdapter.Instance.Execute(EventKey.Resume);
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
