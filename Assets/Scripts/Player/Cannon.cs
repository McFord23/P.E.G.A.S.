using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public bool active = true;
    public float power = 100f;
    public float speed = 1f;

    Vector2 cannonPos;
    Vector2 mousePos;
    Vector3 target;
    float angle;
    public Vector2 direction;

    public SoundController soundController;

    void Start()
    {
        soundController.cannonScratchSound.Play();
    }

    void Update()
    {
        if (active)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.y = Mathf.Clamp(mousePos.y, 1, 55);
            cannonPos = transform.position;

            angle = Vector2.Angle(Vector2.right, mousePos - cannonPos);
            angle = (transform.position.y < mousePos.y) ? angle : -angle;

            target = new Vector3(0f, 0f, Mathf.Clamp(angle, 1, 55));
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, target, Time.deltaTime * speed);

            soundController.cannonScratchSound.volume = Mathf.Abs(target.z / transform.eulerAngles.z - 1);

            direction = transform.right;
            direction.Normalize();
        }
    }

    public void Reset()
    {
        active = true;
        soundController.cannonScratchSound.Play();
    }

    public void Shoot()
    {
        soundController.cannonShootSound.Play();
        soundController.cannonScratchSound.Stop();
        active = false;
    }
}
