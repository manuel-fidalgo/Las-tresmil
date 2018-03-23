using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	void Start () {
		// Destroy(transform.gameObject,5);
	}

	void OnCollisionEnter (Collision col)
	{
		Vector3 hit_position = transform.position;
		

		ApplyDamage(hit_position);
		ExplosionAnimation(hit_position);

		//Destroy(transform.gameObject);
	}

	//Apply the damage to all the elements that are more near than 10 meters
	private void ApplyDamage(Vector3 hit){

		float max_damage_distance = 10;
		float max_damage = 300;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Vehicle");
		GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

		float distance = 0;
		float prot = 0;

		foreach(GameObject e in enemies){
			distance = Vector3.Distance(e.transform.position,hit);
			if(distance < max_damage_distance){
				e.GetComponent<Enemy>().SetDamage(GetDamage(distance,max_damage));
			}
		}
		foreach(GameObject e in vehicles){
			distance = Vector3.Distance(e.transform.position,hit);
			if(distance < max_damage_distance){
				e.GetComponent<VehicleController>().SetDamage(GetDamage(distance,max_damage));
			}
		}
		foreach(GameObject e in player){
			distance = Vector3.Distance(e.transform.position,hit);
			if(distance < max_damage_distance){
				e.GetComponent<MainCharacter>().SetDamage(GetDamage(distance,max_damage));
			}
		}
	}

	//Get the damage relative to the distance
	private float GetDamage(float distance, float max_damage){
		float invense_distance = 0;
		invense_distance = 1/distance; // range (0.1, 1)
		return invense_distance * max_damage;
	}

	//trigger the animation of the explosion
	private void ExplosionAnimation(Vector3 hit){

	}

}
