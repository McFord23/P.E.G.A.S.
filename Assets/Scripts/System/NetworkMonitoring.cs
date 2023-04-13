using System.Collections;
using Enums;
using Unity.Netcode;
using UnityEngine;

public class NetworkMonitoring : SingletonNetworkBehaviour<NetworkMonitoring>
{
    //private bool connected = false;
    private IEnumerator waitingConnection;
    private IEnumerator checkConnection;

    public delegate void ConnectionCompleteEvent();
    public event ConnectionCompleteEvent OnConnectionCompleteEvent;
    
    public delegate void ConnectionFailureEvent();
    public event ConnectionFailureEvent OnConnectionFailureEvent;
    
    public delegate void DisconnectEvent();
    public event DisconnectEvent OnDisconnectEvent;

    public void StartClientConnection()
    {
        NetworkManager.StartClient();
        waitingConnection = WaitingConnection();
        StartCoroutine(waitingConnection);
    }

    public void StopClient()
    {
        StopCoroutine(checkConnection);
        NetworkManager.Shutdown();
        OnDisconnectEvent?.Invoke();
    }

    public override void OnNetworkSpawn()
    {
        StopCoroutine(waitingConnection);

        //checkConnection = CheckConnection();
        //connected = true;
        
        OnConnectionCompleteEvent?.Invoke();
    }
    
    private IEnumerator WaitingConnection()
    {
        yield return new WaitForSeconds(15);
        if (!NetworkManager.IsConnectedClient) OnConnectionFailureEvent?.Invoke();
    }

    public IEnumerator CheckConnection()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            
            if (!NetworkManager.IsConnectedClient)
            {
                print("Is not connected");
                OnDisconnectEvent?.Invoke();
                break;
            }
        }
    }

    /*private bool isConnected => Global.gameMode == GameMode.Host 
        ? NetworkManager. 
        : NetworkManager.IsConnectedClient;*/

    /*[ServerRpc(RequireOwnership = false)]
    private void CheckConnectionServerRpc()
    {
        CheckConnectionClientRpc();
    }
    
    [ClientRpc]
    private void CheckConnectionClientRpc()
    {
        connected = true;
    }*/
}
