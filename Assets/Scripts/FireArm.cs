using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FireArm : MonoBehaviour {

	public string name;

	public List<Magazine> magazinesavailables;
	public GameObject bullet;
	protected GameObject barrel_exit;
	protected ParticleSystem flame;

	public virtual void Update(){

		
	}

	public virtual void Start(){

		barrel_exit = transform.Find("BarrelExit").gameObject;
		flame = barrel_exit.GetComponent<ParticleSystem>();
	}

 	public virtual void Fire(Vector3 shooting_point) {

 		var shooting_direction = (shooting_point - barrel_exit.transform.position).normalized;

 		// Debug.DrawLine(barrel_exit.transform.position, shooting_point, Color.green, 2.0f);
 		 Debug.DrawRay(barrel_exit.transform.position, shooting_direction * 1000, Color.green, 2.0f);

 	 	var bullet_flying = (GameObject)Instantiate (bullet,barrel_exit.transform.position,barrel_exit.transform.rotation);
	 	bullet_flying.GetComponent<Rigidbody>().velocity = shooting_direction * 10;
 	 	Destroy(bullet_flying, 7.0f);
 	}
}
