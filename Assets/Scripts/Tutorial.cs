using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Animator BoardMareAnim;
    public Animator SpaceAnim;

    public GameObject boardMare;
    public GameObject cursor;
    Vector3 startPosition;
    public Vector3 finishPosition;
    Vector3 target;
    public float speed = 0.5f;

    float time;
    public float couldown = 0.1f;

    void Start()
    {
        time = couldown;

        startPosition = cursor.transform.localPosition;
    }

    void Update()
    {
        FlapGuide();
        DirectionGuide();
    }

    void FlapGuide()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            BoardMareAnim.Play("BM Flap");
            SpaceAnim.Play("Press");
            time = couldown;
        }
    }
    
    void DirectionGuide()
    {
        if (cursor.transform.localPosition == startPosition)
        {
            target = finishPosition;
        }
        else if (cursor.transform.localPosition == finishPosition)
        {
            target = startPosition;
        }

        boardMare.transform.right = cursor.transform.position - boardMare.transform.position;
        cursor.transform.localPosition = Vector3.MoveTowards(cursor.transform.localPosition, target, Time.deltaTime * speed);
    }

}
