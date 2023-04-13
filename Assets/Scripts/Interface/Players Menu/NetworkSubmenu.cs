using Enums;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Collections;

public class NetworkSubmenu : NetworkBehaviour
{
    private NetworkMonitoring networkMonitoring;
    private IPFieldManager ipFieldManager;
    private PlayersMenu playersMenu;

    private Text status;
    private Color brown;
    private Color red;

    private Button createButton;
    private Button connectButton;
    private GameObject shutdownButton;
    private GameObject cancelButton;

    private void Start()
    {
        networkMonitoring = NetworkMonitoring.Instance;
        ipFieldManager = GetComponentInChildren<IPFieldManager>();
        playersMenu = GetComponentInParent<PlayersMenu>();

        status = transform.Find("Status").GetComponent<Text>();
        brown = status.color;
        red = new Color(0.45f, 0.2f, 0.15f);

        createButton = transform.Find("Create").GetComponent<Button>();
        connectButton = transform.Find("Connect").GetComponent<Button>();
        shutdownButton = transform.Find("Shutdown").gameObject;
        cancelButton = transform.Find("Cancel").gameObject;
    }

    public void CreateHost()
    {
        Application.logMessageReceived += OnCreatingFailure;

        status.text = "creating...";
        status.color = brown;
        status.gameObject.SetActive(true);

        ipFieldManager.Block(true);

        NetworkManager.StartHost();
    }

    private void OnCreatingFailure(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error && stackTrace.Contains("Netcode"))
        {
            Application.logMessageReceived -= OnCreatingFailure;
            ShowError(logString);
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        Application.logMessageReceived -= OnCreatingFailure;

        if (IsHost)
        {
            Global.gameMode = GameMode.Host;
            ShowStatus("no player");

            createButton.gameObject.SetActive(false);
            connectButton.gameObject.SetActive(false);
            shutdownButton.SetActive(true);
        }
        else if (IsClient)
        {
            print("Client Connected to Host");
            Global.gameMode = GameMode.Client;
            
            status.gameObject.SetActive(false);
            ipFieldManager.Block(false);

            cancelButton.SetActive(false);
            createButton.gameObject.SetActive(true);
            connectButton.gameObject.SetActive(true);

            OnClientConnectedServerRpc();
            StartCoroutine(networkMonitoring.CheckConnection());
        }
    }

    public void Connect()
    {
        ShowStatus("connecting...");

        ipFieldManager.Block(true);

        createButton.gameObject.SetActive(false);
        connectButton.gameObject.SetActive(false);
        cancelButton.SetActive(true);

        NetworkManager.StartClient();
        StartCoroutine("WaitingConnection");
    }

    private void OnConnectingFailure()
    {
        ShowError("connection error");
        Shutdown(true);
    }

    [ServerRpc(RequireOwnership = false)]
    private void OnClientConnectedServerRpc()
    {
        OnClientConnectedClientRpc();
    }

    [ClientRpc]
    private void OnClientConnectedClientRpc()
    {
        Global.playerAmmount = 2;
        playersMenu.UpdatePlayersSubmenu();
        gameObject.SetActive(false);
    }

    public void CancelConnecting()
    {
        StopCoroutine("WaitingConnection");
        Shutdown();
    }

    public void Shutdown(bool statusActive = false)
    {
        NetworkManager.Shutdown();

        status.gameObject.SetActive(statusActive);
        ipFieldManager.Block(false);

        shutdownButton.SetActive(false);
        cancelButton.SetActive(false);
        createButton.gameObject.SetActive(true);
        connectButton.gameObject.SetActive(true);

        Global.gameMode = GameMode.Single;
    }

    public void Disconnect()
    {
        RequestClientDisconnectServerRpc(1);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestClientDisconnectServerRpc(ulong id)
    {
        RequestDisconnectClientRpc();
        NetworkManager.DisconnectClient(id);
    }

    [ClientRpc]
    private void RequestDisconnectClientRpc()
    {
        Global.playerAmmount = 1;

        if (IsHost)
        {
            ShowStatus("no player");
        }
        else if (IsClient)
        {
            HideStatus();

            ipFieldManager.Block(false);

            cancelButton.SetActive(false);
            createButton.gameObject.SetActive(true);
            connectButton.gameObject.SetActive(true);

            StopCoroutine("CheckShutdown");
            NetworkManager.Shutdown();
        }

        playersMenu.Bannish();
        gameObject.SetActive(true);
    }

    public void OnHostDown()
    {
        HideStatus();

        ipFieldManager.Block(false);

        cancelButton.SetActive(false);
        createButton.gameObject.SetActive(true);
        connectButton.gameObject.SetActive(true);

        NetworkManager.Shutdown();

        Global.gameMode = GameMode.Single;
        playersMenu.Bannish();
        gameObject.SetActive(true);
    }

    private void ShowStatus(string text)
    {
        StopCoroutine("HideErrorConnection");

        status.text = text;
        status.color = brown;
        status.gameObject.SetActive(true);
    }

    public void ShowError(string error, bool blockButton = false)
    {
        StopCoroutine("HideErrorConnection");

        status.text = error;
        status.color = red;
        status.gameObject.SetActive(true);
        StartCoroutine("HideErrorConnection");

        if (blockButton)
        {
            createButton.interactable = false;
            connectButton.interactable = false;
        }
    }

    public void HideStatus()
    {
        StopCoroutine("HideErrorConnection");
        status.gameObject.SetActive(false);
        createButton.interactable = true;
        connectButton.interactable = true;
    }

    private IEnumerator HideErrorConnection()
    {
        Color color = status.color;

        yield return new WaitForSeconds(3);

        while (color.a > 0)
        {
            color.a -= 0.001f;
            status.color = color;
            yield return null;
        }
    }
}
