﻿using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.main.duration);
    }
	
}
