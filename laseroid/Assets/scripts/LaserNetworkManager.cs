using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserNetworkManager : NetworkManager
{
    public bool isAtStartup = true;
    private NetworkClient netClient = null;
    private GameObject shipPrefab;
    private bool statusConnection = false;

    NetworkClient myClient;

    // Use this for initialization
    void Start()
    {
        shipPrefab = Resources.Load("ships/pf_dark_fighter_63") as GameObject;
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Host()
    {
        netClient = this.StartHost();
    }

    public void Join(string _IP)
    {
        
        this.networkAddress = _IP;
        netClient = this.StartClient();
        netClient.RegisterHandler(MsgType.Connect, ConnectedHandler);
    }

    public bool GetStatusConnection()
    {
        return statusConnection;
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        Debug.Log("OnStartClient ");
        Debug.Log(client.connection);
        if (client.connection != null)
        {
            Debug.Log("HERARAER");
            statusConnection = true;
        }
    }

    public void ConnectionErrorHandler(NetworkMessage netMsg)
    {
        Debug.Log("Error On connection");
    }

    public void ConnectionErrorHandlerTest(NetworkMessage netMsg)
    {
        Debug.Log("Error On test");
    }

    public void ConnectionErrorInfoHandler(NetworkMessage netMsg)
    {
        Debug.Log("Error On ibfo");
    }    


    public void ConnectedHandler(NetworkMessage netMsg)
    {
        Debug.Log(netMsg);
        Debug.Log(client.connection);
        Debug.Log(client.isConnected);
        if (client.isConnected)
        {
            Debug.Log("HERARAER");
            statusConnection = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        ClientScene.AddPlayer(0);
        
    }
}

    