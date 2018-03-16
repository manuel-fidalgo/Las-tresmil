using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class VehicleController: MonoBehaviour {


	private List<WheelCollider> colliders;
	private List<GameObject> wheels;
    public Cameracontroller cameracontroller;

    private static float maxMotorTorque = 1500f;
    private static float maxSteeringAngle = 30f;

    public void Start(){

    	GetColliders();
    }

    public void FixedUpdate(){

    	float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

         if(Input.GetKeyDown(KeyCode.E))
            FinishDrivingMode();


        foreach(WheelCollider c in colliders){
        	c.motorTorque = motor;
        }
        //steer is only applied to the front wheels
        colliders[0].steerAngle = steering;
        colliders[2].steerAngle = steering;
    }
    public void Update(){

    }

    public void Accelerate() {

    }
    public void Brake() {

    }
    public void Steer() {

    }
    public void ManualBrake() {

    }

    public void GetColliders(){

    	colliders = new List<WheelCollider>();
    	wheels = new List<GameObject>();

    	string collider_format = "Wheel Collider ({0})";
    	string wheel_format = "Wheel ({0})";

    	for(int i = 0; i<4; i++){
    		GameObject wheel, wheel_collider;

    		wheel = transform.Find(string.Format(wheel_format,i)).gameObject;
			wheel_collider = transform.Find(string.Format(collider_format,i)).gameObject;

			Debug.Log(string.Format(wheel_format,i));

    		colliders.Add(wheel_collider.GetComponent<WheelCollider>());
    		wheels.Add(wheel);
    	}
    }

    private void FinishDrivingMode(){

    	GameObject character = transform.Find("MainCharacter").gameObject;
    	MainCharacter mc = character.GetComponent<MainCharacter>();
    	mc.enabled = true;
    	mc.LeaveVehicle(gameObject);
    }

}