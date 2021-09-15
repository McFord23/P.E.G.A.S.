using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamepadMonitoring : MonoBehaviour
{
    public bool[] isGamepadConnected = new bool[2];
    public bool[] oldConnection = new bool[2];

    public UnityEvent GamepadEnabledEvent;
    public UnityEvent GamepadDisabledEvent;

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            try
            {
                isGamepadConnected[i] = (Input.GetJoystickNames()[i] != "") ? true : false;
                if (isGamepadConnected[0]) GamepadEnabledEvent.Invoke();
                oldConnection[i] = isGamepadConnected[i];
            }
            catch
            {
                isGamepadConnected[i] = false;
                oldConnection[i] = isGamepadConnected[i];
            }
        }

        Gamepad.gamepad1 = isGamepadConnected[0];
        Gamepad.gamepad2 = isGamepadConnected[1];
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            try
            {
                isGamepadConnected[i] = (Input.GetJoystickNames()[i] != "") ? true : false;
                if (isGamepadConnected[i] != oldConnection[i])
                {
                    Gamepad.gamepad1 = isGamepadConnected[0];
                    Gamepad.gamepad2 = isGamepadConnected[1];

                    if (isGamepadConnected[i]) GamepadEnabledEvent.Invoke();
                    else GamepadDisabledEvent.Invoke();
                }

                oldConnection[i] = isGamepadConnected[i];
            }
            catch
            {
                isGamepadConnected[i] = false;
                if (isGamepadConnected[i] != oldConnection[i])
                {
                    Gamepad.gamepad1 = isGamepadConnected[0];
                    Gamepad.gamepad2 = isGamepadConnected[1];

                    if (isGamepadConnected[i]) GamepadEnabledEvent.Invoke();
                    else GamepadDisabledEvent.Invoke();
                }

                oldConnection[i] = isGamepadConnected[i];
            }
        }
    }
}
