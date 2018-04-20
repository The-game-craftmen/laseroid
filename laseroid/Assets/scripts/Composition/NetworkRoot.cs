using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkRoot : NetworkManager
{

    void Start()
    {
        // Always try to start Host
        // If another Host is already started on this computer, 
        // the port 7777 will be in use and an error will throw
        //StartHost();
        //GameObject test = GameObject.Find("Player");
        //NetworkServer.Spawn(test);

        // Reduce performances to run X instances on 1 computer without lag
        // TODO Remove this before release
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 210, 580), "Laseroid");

        if (GUI.Button(new Rect(20, 40, 200, 20), "Server"))
        {
            StartHost();
            //GameObject test = GameObject.Find("Player");
            //NetworkServer.Spawn(test);
        }

        if (GUI.Button(new Rect(20, 70, 200, 20), "Client"))
        {
            networkAddress = "127.0.0.1";
            StartClient();
        }

        StringBuilder sb = new StringBuilder("Score").AppendLine().AppendLine();
        List<GameObject> playerObjects = GameObject.FindGameObjectsWithTag("Player").ToList();
        playerObjects.ForEach(playerObject =>
        {
            Player p = playerObject.GetComponent<Player>();
            sb.AppendLine(p.Nickname + " : " + p.Score);
            GameObject shipObject = playerObject.transform.GetChild(0).gameObject;
            Ship s = shipObject.GetComponent<Ship>();
            sb.AppendLine("Hit points" + " : " + s.HitPoints);
            sb.AppendLine();
        });
        //sb.AppendLine().AppendLine("Ships");
        //List<GameObject> ships = GameObject.FindGameObjectsWithTag("Ship").ToList();
        //ships.ForEach(ship =>
        //{
        //    Ship s = ship.GetComponent<Ship>();
        //    sb.AppendLine("" + s.HitPoints);
        //});

        GUI.Label(new Rect(20, 100, 200, 200), sb.ToString());
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        //Debug.Log("LNM update isNetworkActive=" + isNetworkActive);
    }


}
