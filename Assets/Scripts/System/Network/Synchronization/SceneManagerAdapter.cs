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
    [SerializeField] private GameObject loadScreen;

    public void LoadScene(string sceneName)
    {
        switch (Global.gameMode)
        {
            case GameMode.Single:
            case GameMode.LocalCoop:
                SceneManager.LoadScene(sceneName);
                break;

            case GameMode.Host:
            case GameMode.Client:
                if (loadScreen) loadScreen.SetActive(true);
                RequestLoadSceneServerRpc(sceneName);
                break;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestLoadSceneServerRpc(string sceneName)
    {
        LoadSceneClientRpc(sceneName);
    }

    [ClientRpc]
    private void LoadSceneClientRpc(string sceneName)
    {
        if (loadScreen) loadScreen.SetActive(true);

        if (Global.gameMode == GameMode.Host)
        {
            NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}