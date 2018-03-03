using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShootingFireArm  : FireArm{


	public override void Fire(Vector3 shooting_point){
		base.Fire(shooting_point);
		FireAnimation();
	}
	public void FireAnimation(){

		if(!flame.isPlaying){
			flame.Play();
		}
	}


}