using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class LevelGameObject : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
        Debug.Log("Start LevelGAmeObject 1");
        if (!isLocalPlayer)
        {
            return;
        }
        Debug.Log("Start LevelGAmeObject 2" );
        GameObject ship = Resources.Load("ships/pf_dark_fighter_63") as GameObject;
        NetworkServer.Spawn(ship);
        Debug.Log("Start LevelGAmeObject");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
