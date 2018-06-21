using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine;


public class MainCharacterVR : MainCharacter{

    // Use this for initialization
	private static bool ready_to_shoot;




    public GameObject handsmodel;
    public GameObject barrel;

    HandModel hand_model;
    Hand leap_hand;

    void Start () {

        base.Start();
        StartFingerTraker();
        cameraObject = transform.Find("Leap Rig").gameObject.transform.Find("Main Camera").gameObject;
       
    }

    public override void Update()
	{
		OVRInput.Update();
        if (!dead)
        {
			DispatchOculusInput();
			CheckGestures();
        }
        ComputeFallingDamage();
        UpdateShootingPointHandBased();
    }

	void FixedUpdate(){
		//OVRInput.FixedUpdate();
		//if (!dead) {
		//	DispatchOculusInput();
		//}
			
	}
		

    private void DispatchOculusInput()
    {

		//Debug.Log (OVRInput.GetConnectedControllers ());
		//Debug.Log (OVRInput.GetActiveController() );

		var sts = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
		Debug.Log(sts);

		if (sts [0] != 0) {
			RotateCharacter (0.9f * sts [0]);
		}
		var vert = 0.3f * sts [1];

		if (sts [1] > 0) {
			transform.Translate(0, 0, vert);
		} else if (sts [1] < 0) {
			transform.Translate(0, 0, vert);
		}

		if (OVRInput.GetDown(OVRInput.Button.Three)) {
			Jump();
		}

        //button four jump
        //button tree take/leave car
    }

	private void CheckGestures() {

		//This method checks if the trigger is pressed to to dath the angles of the index finger are analyzed
		FingerModel finger = hand_model.fingers[1];
		float f2 = finger.GetFingerJointStretchMecanim(1);

		//Debug.Log("f1 "+f1+" f2 "+f2+" f3 "+f3);
		//78.0 limit value.

		if(f2 < -78.0f & ready_to_shoot) {
			Debug.Log("Shooting");
			ready_to_shoot = false;
			FireArm arm = fire_arms[0].GetComponent<FireArm>();
			arm.TriggerFire();
		}

		if(f2 > -68.0f) {
			ReadyToShoot();
		}
	}

	private void CheckDriving() {
		Transform palm = hand_model.wristJoint;
		Debug.Log("Palm->"+ palm.localEulerAngles);

	}

	private void ReadyToShoot(){
		ready_to_shoot = true;
	}


    private void UpdateShootingPointHandBased()
    {

        //This point will be updated with the leap motion
        int index_finger = 1;
        GameObject weapon = fire_arms[weapon_in_use];
        FireArm controller = weapon.GetComponent<FireArm>();
        controller.UpdateShootingPoint(GetShootingPoint());

    }

    public void StartFingerTraker() {

        hand_model = handsmodel.GetComponent<HandModel>();
        leap_hand = hand_model.GetLeapHand();

        if (leap_hand == null)
            Debug.Log("No leap_hand founded");

    }
		
    //gets the shooting point from the end of the barrel
    public Vector3 GetShootingPoint() {

        Vector3 position = barrel.transform.position;
        Vector3 direction = barrel.transform.forward;

        Ray ray = new Ray(position,direction);
        Debug.DrawRay(position,direction, Color.red);


        RaycastHit hit;
        Vector3 shooting_point;

        bool collision = Physics.Raycast(ray, out hit);

        if (collision) {
            shooting_point = hit.point;
        }
        else {
            int infinite_distance = 100;
            shooting_point = ray.origin + (ray.direction.normalized * infinite_distance);
        }
        return shooting_point;
    }

    //Draws a line straight forward from the index finguer
    public Vector3 getShootingFingerPoint(int index) {

        FingerModel finger = hand_model.fingers[index];
        Debug.DrawRay(finger.GetTipPosition(), finger.GetRay().direction, Color.red);

        Ray finger_ray = new Ray(finger.GetTipPosition(), finger.GetRay().direction);

        RaycastHit hit;
        Vector3 shooting_point;

        bool collision = Physics.Raycast(finger_ray, out hit);

        if (collision) {
          
            shooting_point = hit.point;
        }
        else {
            int infinite_distance = 100;
            shooting_point = finger_ray.origin + (finger_ray.direction.normalized * infinite_distance);
        }

        return shooting_point;
    }

}
