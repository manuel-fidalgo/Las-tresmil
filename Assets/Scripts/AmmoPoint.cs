﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 100 * Time.deltaTime, 0, Space.World);
	}

	void OnCollisionEnter(Collision collision){
		Destroy(transform.gameObject);
	}
}
