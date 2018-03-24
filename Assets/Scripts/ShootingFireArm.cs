using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShootingFireArm  : FireArm{


	public override void Fire(){
		base.Fire();
		FireAnimation();
	}

}