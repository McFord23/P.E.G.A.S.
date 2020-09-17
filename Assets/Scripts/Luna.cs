using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luna : MonoBehaviour
{
    Animator animatorController;
    MoveState moveState;

    float flapTime = 0;
    float flapCooldown = 0.375f;

    public enum MoveState
    {
        FreeFall,
        Flap
    }

    void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Flap();
        }
    }

    void FixedUpdate()
    {
        if (moveState == MoveState.Flap)
        {
            flapTime -= Time.deltaTime;
            if (flapTime <= 0)
            {
                FreeFall();
            }
        }
    }

    public void FreeFall()
    {
        moveState = MoveState.FreeFall;
        animatorController.Play("LunaFreeFall");
    }

    public void Flap()
    {
        moveState = MoveState.Flap;
        flapTime = flapCooldown;
        animatorController.Play("LunaFlap");
    }
}
