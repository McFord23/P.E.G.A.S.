using UnityEngine;
using UnityEngine.Events;

public class Heart : MonoBehaviour
{
    GameObject target;
    Vector3 spawnPos = new Vector3(25, 23, 0);
    public UnityEvent VictoryEvent;

    void Start()
    {
        transform.position = spawnPos;
    }

    void Update()
    {
        if (target != null) transform.position = target.transform.position;
    }

    public void Reset()
    {
        target = null;
        transform.position = spawnPos;
    }

    public void DropTarget()
    {
        target = null;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (target == null)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.transform.parent.GetComponent<Player>().moveState != MoveState.Dead)
                {
                    target = collider.gameObject;
                }
            }
            else if (collider.gameObject.CompareTag("Windigo"))
            {
                if (collider.gameObject.GetComponent<Windigo>().GetMoveState() != Windigo.MoveState.Dead)
                {
                    target = collider.gameObject;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Finish"))
        {
            VictoryEvent.Invoke();
        }
    }
}
