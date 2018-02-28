using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FireArm : MonoBehaviour {

	public string name;

	public List<Magazine> magazinesavailables;
	public GameObject bullet;



	public void Start(){

	}
	public void Reload() {

	}

// 	public void Fire(Vector3 direction) {

// 		var bullet_flying = (GameObject)Instantiate (bullet,transform.position,transform.rotation);
// 		bullet_flying.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
// 		Destroy(bullet_flying, 2.0f);
// 	}

// 	public void Fire() {

// 		var bullet_flying = (GameObject)Instantiate (bullet,transform.position,transform.rotation);
// 		bullet_flying.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
// 		Destroy(bullet_flying, 2.0f);
// 	}
}
