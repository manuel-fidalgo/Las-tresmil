using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MainCharacter : Character {


	public List<GameObject> fire_arms;
	public int weapon_in_use;

	public static int ROTATION_SPEED = 5;
	public GameObject cameraObject;

	public static float FORWARD_SPEED = 9f;
	public static float LATERALBACKWARD_SPEED = 5f;

    EnemiesManager enemies;


    public void Awake() {

		cameraObject = transform.Find("Camera").gameObject;
 
    }

	public void FixedUpdate() {

        float angle_x = ROTATION_SPEED * Input.GetAxis("Mouse X");
		float angle_y = ROTATION_SPEED * Input.GetAxis("Mouse Y");

		DispatchKeyInput();
		RotateCharacter(angle_x);
		RotateCamera(angle_y);

	}
	private void RotateCharacter(float angle) {
		trans.Rotate(trans.up,angle);
	}

	private void RotateCamera(float angle) {

		float rotation_x = cameraObject.transform.rotation.eulerAngles.x;

        // Debug.Log(rotation_x + "##" + angle);s

         //Camera goes up
		if(angle > 0){

			if(rotation_x < 30 || rotation_x > 300){
				cameraObject.transform.RotateAround(trans.position, trans.right, angle);
			}


        }else{ //camera does down

        	if(rotation_x > 350 || rotation_x < 90){
        		cameraObject.transform.RotateAround(trans.position, trans.right, angle);
        	}
        }

    }

    public void DispatchKeyInput() {


    	float vert, hor = 0f;

    	if(Input.GetAxis("Vertical") > 0)
    		vert = Input.GetAxis("Vertical") * FORWARD_SPEED * Time.deltaTime;
    	else
    		vert = Input.GetAxis("Vertical") * LATERALBACKWARD_SPEED * Time.deltaTime;

    	hor = Input.GetAxis("Horizontal") * LATERALBACKWARD_SPEED * Time.deltaTime;

    	transform.Translate(0, 0, vert);
    	transform.Translate(hor, 0, 0);

    	if(Input.GetKey(KeyCode.Space)){
    		Jump();
    	}else if(vert > 0){
    		MoveForward();
    	}else if (vert < 0){
    		MoveBackward();
    	}else if(hor > 0){
    		MoveRigth();
    	}else if(hor < 0){
    		MoveLeft();
    	}else if(Input.GetKey(KeyCode.LeftShift)){
    		Knee();
    	}else{
    		Idle();
    	}

    	if(Input.GetKeyDown(KeyCode.R))
    		ChangeWeapon();

    	if(Input.GetMouseButtonDown(0))
    		// FireArmAction();
  

    	
    	IsGrounded();
    	ApplyAnimationParams();


    }

    private void ChangeWeapon() {

    	fire_arms[weapon_in_use].SetActive(false);
    	weapon_in_use++;
    	if (weapon_in_use >= 4){
    		weapon_in_use = 0;
    	}
    	fire_arms[weapon_in_use].SetActive(true);

    	if(weapon_in_use == 0){
    		TriggerAnimation("Pistol");
    	}else{
    		TriggerAnimation("Rifle");
    	}
    }

    //Manages actions such shoot, reload etc..
    // private void FireArmAction() {

    // 	GameObject arm = fire_arms[weapon_in_use];
    // 	FireArm fa = arm.GetComponent<FireArm>();
    // 	fa.Fire();
    // }

}