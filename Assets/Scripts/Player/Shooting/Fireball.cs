using UnityEngine;

public class Fireball : MonoBehaviour
{
    private GameObject fireball;

    private void Start()
    {
        fireball = transform.gameObject;
        Invoke("AutoDestroy", 1.0f);
    }

    private void AutoDestroy()
    {
        Destroy(fireball);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(fireball);
    }
}