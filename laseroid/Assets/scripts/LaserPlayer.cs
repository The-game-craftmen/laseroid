using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class LaserPlayer : NetworkBehaviour{
    private string name;

    

	// Use this for initialization
	void Start () {
        Debug.Log("@@@@@@@@@@@@@@@@@@@@");
        Debug.Log(isLocalPlayer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetName(string _name)
    {
        name = _name;
    }
}
