using Enums;
using Unity.Netcode;
using UnityEngine.SceneManagement;

/**
 * Абстрагирует мультиплеерный и одиночный переход между сценами 
 * Должен использоваться везде вместо обычного
 */
public class SceneManagerAdapter
{
    private static bool IsMultiplayer => 
        Save.gameMode != GameMode.Single && 
        (NetworkManager.Singleton.IsClient || NetworkManager.Singleton.IsHost);

    public static void LoadScene(string sceneName)
    {
        if (IsMultiplayer)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}