using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserNetworkManager : NetworkManager
{
    public bool isAtStartup = true;
    private NetworkClient netClient = null;

    NetworkClient myClient;

    // Use this for initialization
    void Start()
    {
        GameObject dataOverScene = GameObject.Find("/DataOverScene");
        if (dataOverScene != null)
        {
            Debug.Log("data over Scene found");
            DataOverScene dataOverSceneScript = dataOverScene.GetComponent<DataOverScene>();
            if (dataOverSceneScript != null)
            {
                Debug.Log("data over Scene Script found");
                if (dataOverSceneScript.IsHost())
                {
                    netClient = this.StartHost();
                }
                else
                {
                    Debug.Log(dataOverSceneScript.GetIp());
                    this.networkAddress = dataOverSceneScript.GetIp();
                    netClient = this.StartClient();
                }

            }
            else
            {
                Debug.Log("data over Scene Script not found");
            }
        }
        else
        {
            Debug.Log("data over Scene not found");
        }

    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
        Debug.Log("Error Connection");
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        Debug.Log(client.connection);
        Debug.Log(client.isConnected);
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

    