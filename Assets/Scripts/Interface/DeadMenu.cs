using UnityEngine;
using UnityEngine.Events;

public class DeadMenu : MonoBehaviour
{
    private GameObject menu;
    private UnityEvent MenuDisabledEvent;
    
    [SerializeField]
    private PlayersManager playersManager;

    private void Start()
    {
        menu = transform.parent.parent.gameObject;
        MenuDisabledEvent = menu.GetComponent<MenuController>().MenuDisabledEvent;
    }

    private void Update()
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
