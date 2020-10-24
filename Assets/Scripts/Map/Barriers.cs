using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriers : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (player.moveState == Player.MoveState.Loaded)
        {
            Reset();
        }
    }

    public void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponentInChildren<BarrierDinamic>().Reset();
        }
    }

    public void Pause(bool check)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponentInChildren<BarrierDinamic>().Pause(check);
        }
    }
}
