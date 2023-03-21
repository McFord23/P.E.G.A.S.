using Enums;
using Unity.Netcode;
using UnityEngine;

/**
 * Временный скрипт для тестирования клиента/хоста, пока отсутствует менюшка
 * TODO: удалить, когда менюшка появится
 */
public class NetworkDebug : MonoBehaviour
{
    private NetworkManager networkManager;

    [SerializeField] private bool debugMode = true;
    [SerializeField] private GameMode gameMode;

    private void Start()
    {
        if (!debugMode)
        {
            return;
        }
        
        networkManager = NetworkManager.Singleton;
        Save.gameMode = gameMode;

        if (networkManager.IsClient || networkManager.IsHost)
        {
            return;
        }

        switch (gameMode)
        {
            case GameMode.Host:
                networkManager.StartHost();
                break;
            case GameMode.Client:
                networkManager.StartClient();
                break;
        }
    }
}
