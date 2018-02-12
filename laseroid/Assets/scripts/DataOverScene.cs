using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOverScene : MonoBehaviour {
    private bool isHost = false;
    private string ip = "127.0.0.1";
    private string nickName = "Joueur1";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetNickname(string _nickname)
    {
        nickName = _nickname;

    }

    public string GetNickname()
    {
        return nickName;
    }

    public void SetIp(string _ip)
    {
        ip = _ip;
    }

    public string GetIp()
    {
        return ip;
    }

    public void SetIsHost(bool _isHost)
    {
        this.isHost = _isHost;
    }

    public bool IsHost()
    {
        return this.isHost;
    }



    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
}
