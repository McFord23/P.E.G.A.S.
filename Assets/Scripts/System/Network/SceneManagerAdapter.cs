using Enums;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Абстрагирует мультиплеерный и одиночный переход между сценами 
 * Должен использоваться везде вместо обычного
 */
public class SceneManagerAdapter : SingletonNetworkBehaviour<SceneManagerAdapter>
{
    public void LoadScene(string sceneName)
    {
        switch (Global.gameMode)
        {
            case GameMode.Single:
            case GameMode.LocalCoop:
                SceneManager.LoadScene(sceneName);
                break;

            case GameMode.Client:
                RequestLoadSceneServerRpc();
                break;

            case GameMode.Host:
                NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                break;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestLoadSceneServerRpc()
    {
        print("request accept");
        LoadSceneClientRpc();
    }

    [ClientRpc]
    private void LoadSceneClientRpc()
    {
        print(NetworkManager.LocalClientId + " is Host: " + IsHost);
        print(NetworkManager.LocalClientId + " GameMode: " + Global.gameMode);

        if (IsHost || Global.gameMode == GameMode.Host)
        {
            NetworkManager.SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
}