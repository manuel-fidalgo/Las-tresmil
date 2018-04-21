using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticFireArm : FireArm {

	
	// Update is called once per frame
	public override void Update () {
		if(Input.GetMouseButton(0)){
			TryFire();
		}
		TextScreen();
	}
	
	public override void Fire(){
		base.Fire();
		FireAnimation();
	}
}
