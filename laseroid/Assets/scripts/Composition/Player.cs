using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SyncVar]
    public int Score;
    [SyncVar]
    public string Nickname;
    //[SyncVar]
    //public GameObject Ship;

    void Start () {
    }
	
	void Update () {		
	}

    [Command]
    public void CmdSetScore(int s)
    {
        Score = s;
    }

    [Command]
    public void CmdSetNickname(string n)
    {
        Nickname = n;
    }

    public override void OnStartLocalPlayer()
    {
        CmdSetScore(Random.Range(1, 1000));
        CmdSetNickname("Player " + GameObject.FindGameObjectsWithTag("Player").Length);
    }
}
