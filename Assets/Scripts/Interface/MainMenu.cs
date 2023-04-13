using Unity.Netcode;
using UnityEngine;

public class MainMenu : NetworkBehaviour
{
    public void Play()
    {
        print("is client: " + IsClient);

        if (IsClient)
        {
            RequestLoadSceneServerRpc();
        }
        else
        {
            SceneManagerAdapter.LoadScene("Game");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestLoadSceneServerRpc()
    {
        SceneManagerAdapter.LoadScene("Game");
    }
}
