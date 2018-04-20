using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ship : NetworkBehaviour {

    [SyncVar]
    public int HitPoints;

    void Start () {
        HitPoints = Random.Range(1, 1000);
        //if (true)
        //{
        //    CmdSetHitPoints(Random.Range(1, 1000));
        //}
    }
	void Update () {
	}

    [Command]
    public void CmdSetHitPoints(int h)
    {
        HitPoints = h;
    }

    public override void OnStartLocalPlayer()
    {
        CmdSetHitPoints(Random.Range(0, 1000));
    }

}
