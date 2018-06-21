using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FireArm : MonoBehaviour {


	public List<Magazine> magazinesavailables = new List<Magazine>();

	public GameObject bullet;
	protected GameObject barrel_exit;
	protected ParticleSystem flame;
	protected Vector3 shooting_point;


	protected int damageBullet = 20;
	public Text txt;

	public virtual void Update(){

		if(Input.GetMouseButtonDown(0)){
			TryFire();
		}
		TextScreen();
	}

	public void TriggerFire() {
		//TryFire();
		Fire ();
		TextScreen();
	}


	public void TryFire(){


		if(magazinesavailables.Count > 0){
			Fire();
			UpdateManagzine();
		}
	}

	public virtual void Start(){

		barrel_exit = transform.Find("BarrelExit").gameObject;
		flame = barrel_exit.GetComponent<ParticleSystem>();

	}

	//Default fire methods, overrided by the shotgun and by the bazooka.
	public virtual void Fire() {

		var shooting_direction = (shooting_point - barrel_exit.transform.position).normalized;

 		// Debug.DrawLine(barrel_exit.transform.position, shooting_point, Color.green, 2.0f);
		Debug.DrawRay(barrel_exit.transform.position, shooting_direction * 1000, Color.green, 2.0f);

		RaycastHit hit;
		var hitbool = Physics.Raycast(barrel_exit.transform.position, shooting_direction, out hit);
		if(hitbool && hit.transform.tag == "Enemy"){
			hit.transform.gameObject.GetComponent<Enemy>().SetDamage(damageBullet);
		}
		if(hitbool && hit.transform.tag == "Vehicle"){
			hit.transform.gameObject.GetComponent<VehicleController>().SetDamage(damageBullet);
		}

	}


	private void UpdateManagzine(){

		Magazine mag = magazinesavailables[0];
		mag.RemoveBullet();

		if(mag.IsEmpty()){
			magazinesavailables.Remove(mag);
		}
	}

	public void TextScreen(){

		String format = "{0}/{1} | {2}";
		String text_screen = "";
		Magazine mag;


		if(magazinesavailables.Count > 0){
			mag = magazinesavailables[0];
			text_screen = String.Format(format, mag.currentbullets, mag.maxbullets, magazinesavailables.Count - 1);
		}else{
			text_screen = String.Format(format, 0, 0, magazinesavailables.Count);
		}

		txt.text = text_screen;
	}

	public void UpdateShootingPoint(Vector3 point){
		shooting_point = point;
	}

	public void FireAnimation(){

		if(!flame.isPlaying){
			flame.Play();
		}
	}

	public void AddMagazine(Magazine mag){
		magazinesavailables.Add(mag);
	}
}
