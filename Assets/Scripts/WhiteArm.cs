using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteArm : MonoBehaviour {

	// Use this for initialization
	public float damage;

	void Start () {
		
		string name = transform.gameObject.name;
		int desviation = 0;

		//the desviation
		if(name == "Wrench"){
			desviation = 10;
		}else if(name == "Bat"){
			desviation = 15;
		}else{
			desviation = 25;
		}

		float max_damage = 50;
		float factor = EnemiesManager.LEVEL/EnemiesManager.MAX_LEVEL;
		damage = factor * max_damage + desviation;

	}


	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.transform.tag == "Player")
		{
			col.gameObject.GetComponent<MainCharacter>().SetDamage(damage);
		}
	}
}
