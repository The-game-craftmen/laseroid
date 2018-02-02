using UnityEngine;
using System.Collections;

public class explosionScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.main.duration);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
