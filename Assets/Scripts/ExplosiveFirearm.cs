using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ExplosiveFirearm : FireArm {


	public override void Fire(Vector3 shooting_point){
		

		GameObject new_bullet = Instantiate(bullet,bullet.transform.position,bullet.transform.rotation);
		var shooting_direction = (shooting_point - barrel_exit.transform.position).normalized;

		// Debug.DrawRay(new_bullet.transform.position,shooting_direction * 100,Color.blue, 200);

		new_bullet.transform.parent = null;
		Rigidbody bulletRigidBody = new_bullet.AddComponent<Rigidbody>(); // Add the rigidbody.
		new_bullet.GetComponent<MeshCollider>().enabled = true;
		new_bullet.GetComponent<Rocket>().enabled = true;

		bulletRigidBody.velocity = shooting_direction * 50;
		bulletRigidBody.transform.localScale += new Vector3(0.9f, 0.9f, 0.9f);

	}
	
}