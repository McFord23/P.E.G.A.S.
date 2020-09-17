using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public bool isPaused = false;
    public float power = 100f;
    public float angle;
    public Vector2 direction;
    Vector2 mousePos;
    Vector2 cannonPos;

    AudioSource shootSound;

    private void Start()
    {
        shootSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isPaused)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.y = Mathf.Clamp(mousePos.y, 0, 55);
            cannonPos = transform.position;

            angle = Vector2.Angle(Vector2.right, mousePos - cannonPos);
            angle = (transform.position.y < mousePos.y) ? angle : -angle;

            transform.eulerAngles = new Vector3(0f, 0f, Mathf.Clamp(angle, 0, 55));

            direction = transform.right;
            direction.Normalize();
        }
    }

    public void Shoot()
    {
        shootSound.Play();
    }
}
