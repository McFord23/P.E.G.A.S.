using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Сredits : MonoBehaviour
{
    public float speed = 0.05f;
    public GameObject skip;

    void Start()
    {
        skip.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y < 30) transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
        else Exit();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            skip.SetActive(true);
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
