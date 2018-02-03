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

    