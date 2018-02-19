﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserNetworkManager : NetworkManager
{
    public bool isAtStartup = true;
    private NetworkClient netClient = null;
    private GameObject shipPrefab;
    private bool statusConnection = false;//0; // -1 error on connection, 0 not yet connected, 1 connected
    private bool isHost = false;
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
        Debug.Log("HOST Here");
        isHost = true;
        netClient = this.StartHost();
        
    }

    public void Join(string _IP)
    {
        
        this.networkAddress = _IP;
        netClient = this.StartClient();
    }

    public void StopClient()
    {
        if (!isHost) { 
            netClient.Disconnect();
        }
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
            Debug.Log("OnStartClient Connection Not null");
            statusConnection = true;
        }
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("OnClientDisconnect");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("OnClientConnect ");
        Debug.Log(client.connection);
        if (client.connection != null)
        {
            Debug.Log("OnClientConnect Connection Not null");
            statusConnection = true;
        }
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
        Debug.Log("OnClientError ");
    }

    public override void OnClientNotReady(NetworkConnection conn)
    {
        base.OnClientNotReady(conn);
        Debug.Log("OnClientNotReady ");
    }


   
}

    