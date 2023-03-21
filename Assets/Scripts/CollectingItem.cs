using UnityEngine;
using UnityEngine.Events;

public class CollectingItem : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(25, 23, 0);
    private Quaternion spawnRot;

    private BoxCollider2D trigger;

    public UnityEvent VictoryEvent;

    private void Start()
    {
        trigger = GetComponent<BoxCollider2D>();

        transform.position = spawnPos;
        spawnRot = transform.rotation;
    }

    public void Reset()
    {
        transform.parent = null;

        enabled = true;
        trigger.enabled = true;

        transform.position = spawnPos;
        transform.rotation = spawnRot;
    }

    public void Pickup(Transform character)
    {
        if (transform.parent == null)
        {
            transform.parent = character;
            transform.position = character.position;
        }
    }

    public void Drop()
    {
        transform.parent = null;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Finish"))
        {
            VictoryEvent.Invoke();
        }
    }
}
