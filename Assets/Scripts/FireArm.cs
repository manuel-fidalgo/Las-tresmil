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
	protected static int damageBullet = 20;

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

		RaycastHit hit;
		var hitbool = Physics.Raycast(barrel_exit.transform.position, shooting_direction, out hit);
		if(hitbool && hit.transform.tag == "Enemy"){
			hit.transform.gameObject.GetComponent<Enemy>().SetDamage(damageBullet);
		}

	}
}
