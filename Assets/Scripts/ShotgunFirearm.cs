using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunFirearm : FireArm {

	float new_damageBullet = 7.5f;

	//The shotgun shoots many small bullets in random directions inside a given angle
	public override void Fire(){
		
		Vector3 shooting_direction_orgininal = (shooting_point - barrel_exit.transform.position).normalized;
		Vector3 shooting_direction = shooting_direction_orgininal;

 		// Debug.DrawLine(barrel_exit.transform.position, shooting_point, Color.green, 2.0f);
 		for(int i=0; i< 15; i++){

 			float rot0 = Random.Range(-3f, 3f);
 			float rot1 = Random.Range(-3f, 3f);
 			float rot2 = Random.Range(-3f, 3f);
 			shooting_direction = Quaternion.Euler(rot0, rot1, rot2) * shooting_direction_orgininal;

			Debug.DrawRay(barrel_exit.transform.position, shooting_direction * 1000, Color.green, 2.0f);

			RaycastHit hit;
			var hitbool = Physics.Raycast(barrel_exit.transform.position, shooting_direction, out hit);
			if(hitbool && hit.transform.tag == "Enemy"){
				hit.transform.gameObject.GetComponent<Enemy>().SetDamage(new_damageBullet);
			}
			if(hitbool && hit.transform.tag == "Vehicle"){
				hit.transform.gameObject.GetComponent<VehicleController>().SetDamage(new_damageBullet);
			}
		}
		FireAnimation();
	}
}
