using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOverScene : MonoBehaviour {
    private bool isHost = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
