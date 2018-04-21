using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPoint : MonoBehaviour {


	public FireArm fa;

	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 100 * Time.deltaTime, 0, Space.World);
	}


	void OnCollisionEnter(Collision collision){

		if(collision.gameObject.tag == "Player"){

			string name = transform.gameObject.name;
			Magazine m;
			GameObject go;

			if(name.Contains("Pistol")){

				m = Magazine.PistolMagazine();
				fa.AddMagazine(m);
			}else if(name.Contains("Rifle")){

				m = Magazine.RifleMagazine();
				fa.AddMagazine(m);
			}else if(name.Contains("Shotgun")){

				m = Magazine.ShotgunMagazine();
				fa.AddMagazine(m);
			}else{

				m = Magazine.BazookaMagazine();
				fa.AddMagazine(m);
			}
			Destroy(transform.gameObject);
		}
	}
}
