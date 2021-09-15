using UnityEngine;

public class Fireball : MonoBehaviour
{
    GameObject fireball;

    void Start()
    {
        fireball = transform.gameObject;
        Invoke("AutoDestroy", 1.0f);
    }

    void AutoDestroy()
    {
        Destroy(fireball);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Fireball") && !collider.gameObject.CompareTag("Layer") && !collider.gameObject.CompareTag("Heart"))
        {
            Destroy(fireball);
        }
    }
}