using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;
using Enums;

public class GamepadMonitoring : NetworkBehaviour
{
    public bool[] isGamepadConnected = new bool[2];
    public bool[] oldConnection = new bool[2];

    public UnityEvent GamepadEnabledEvent;
    public UnityEvent GamepadDisabledEvent;

    private NetworkVariable<bool> gamepad1 = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<bool> gamepad2 = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            try
            {
                isGamepadConnected[i] = (Input.GetJoystickNames()[i] != "") ? true : false;
                if (isGamepadConnected[i]) GamepadEnabledEvent.Invoke();
            }
            catch
            {
                isGamepadConnected[i] = false;
            }

            oldConnection[i] = isGamepadConnected[i];
        }

        Gamepad.gamepad1 = isGamepadConnected[0];
        Gamepad.gamepad2 = isGamepadConnected[1];
    }

    private void Update()
    {
        switch (Global.gameMode)
        {
            case GameMode.Single:
            case GameMode.LocalCoop:
                LocalMonitoring();
                break;

            case GameMode.Host:
                HostMonitoring();
                break;

            case GameMode.Client:
                ClientMonitoring();
                break;
        }
    }

    private void LocalMonitoring()
    {
        for (int i = 0; i < 2; i++)
        {
            try
            {
                isGamepadConnected[i] = (Input.GetJoystickNames()[i] != "") ? true : false;
            }
            catch
            {
                isGamepadConnected[i] = false;
            }

            if (isGamepadConnected[i] != oldConnection[i])
            {
                Gamepad.gamepad1 = isGamepadConnected[0];
                Gamepad.gamepad2 = isGamepadConnected[1];

                if (isGamepadConnected[i]) GamepadEnabledEvent.Invoke();
                else GamepadDisabledEvent.Invoke();

                oldConnection[i] = isGamepadConnected[i];
            }
        }
    }

    private void HostMonitoring()
    {
        bool gamepad1Status;
        bool gamepad2Status = gamepad2.Value;

        try
        {
            gamepad1Status = (Input.GetJoystickNames()[0] != "") ? true : false;
        }
        catch
        {
            gamepad1Status = false;
        }

        gamepad1.Value = gamepad1Status;
        CheckChangeStatus(gamepad1Status, gamepad2Status);
    }

    private void ClientMonitoring()
    {
        bool gamepad1Status = gamepad1.Value;
        bool gamepad2Status;

        try
        {
            gamepad2Status = (Input.GetJoystickNames()[0] != "") ? true : false;
        }
        catch
        {
            gamepad2Status = false;
        }

        gamepad2.Value = gamepad2Status;
        CheckChangeStatus(gamepad1Status, gamepad2Status);
    }

    private void CheckChangeStatus(bool newGamepad1Status, bool newGamepad2Status)
    {
        if (newGamepad1Status != Gamepad.gamepad1)
        {
            Gamepad.gamepad1 = newGamepad1Status;

            if (Gamepad.gamepad1) GamepadEnabledEvent.Invoke();
            else GamepadDisabledEvent.Invoke();
        }

        if (newGamepad2Status != Gamepad.gamepad2)
        {
            Gamepad.gamepad2 = newGamepad1Status;

            if (Gamepad.gamepad2) GamepadEnabledEvent.Invoke();
            else GamepadDisabledEvent.Invoke();
        }
    }
}
