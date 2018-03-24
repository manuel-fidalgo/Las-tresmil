using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class VehicleController: MonoBehaviour {


	private List<WheelCollider> colliders;
	private List<GameObject> wheels;
	private GameObject camera;
	public Material mat;

	private static float maxMotorTorque = 1500f;
	private static float maxSteeringAngle = 30f;
	private static float maxBrakeTorque = 1000f;

	private float health = 200;


	float oldSteering = 0;
	float currentSteering = 0;

	private int ROTATION_SPEED = 5;

	public void Start(){

		GetColliders();
		SetCamera();

		GetComponent<Rigidbody>().centerOfMass += new Vector3(0, -2.0f, 1.0f);

	}

	public void FixedUpdate(){

		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float Horizontal_axis = maxSteeringAngle * Input.GetAxis("Horizontal");

		foreach(WheelCollider c in colliders){
			c.motorTorque = motor;
		}
        //steer is only applied to the front wheels
		colliders[0].steerAngle = Horizontal_axis;
		colliders[2].steerAngle = Horizontal_axis;

		Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
	}

	public void Update(){

		CameraRotation();
		WheelVisualEfects();

		if(Input.GetKeyDown(KeyCode.E))
		FinishDrivingMode();
		if (Input.GetKeyDown(KeyCode.Space))
		Brake();
		else
		UnBrake();

	}
	//Wheels rotation
	public void WheelVisualEfects(){

		float rpm = colliders[0].rpm;

		foreach(GameObject wheel in wheels)
		{
			wheel.transform.Rotate(rpm/60*360 * Time.deltaTime ,0,0);
		}
	}

	//gets the camera objet
	public void SetCamera(){
		camera = transform.Find("Camera").gameObject;

	}

	//Remove brake forces from the car
	public void UnBrake() {

		colliders[1].brakeTorque = 0;
		colliders[3].brakeTorque = 0;
	}
	//Add brake forces
	public void Brake() {
		colliders[1].brakeTorque = maxBrakeTorque;
		colliders[3].brakeTorque = maxBrakeTorque;
	}

	public void CameraRotation(){
		Transform car; 
		float angle_x = 0;

		angle_x = ROTATION_SPEED * Input.GetAxis("Mouse X");
		camera.transform.RotateAround(transform.position, transform.transform.up, angle_x);
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

	//Get the player out of the car.
	private void FinishDrivingMode(){

		GameObject character = transform.Find("MainCharacter").gameObject;
		MainCharacter mc = character.GetComponent<MainCharacter>();
		mc.enabled = true;

		camera.SetActive(false);
		mc.LeaveVehicle(gameObject);

	}

	public void SetDamage(float amount){

		Debug.Log("amount->"+ (int) amount + "health->" + health );
        health = health - (int) amount;
        Debug.Log("amount->"+ (int) amount + "health->" + health );
        if(health <= 0){
            Explode();
        }
    }

    private void Explode(){

    	try{
    		FinishDrivingMode();
    	}catch(NullReferenceException e){
    		//There is no driver in the vehicle.	
    	}
		transform.gameObject.tag = "BrokenVehicle";

		Transform driver_pos = transform.Find("DriverPosition");
		GameObject explosion = GameObject.Find("Flames");
		GameObject new_newexplosion = Instantiate(explosion, driver_pos.position, Quaternion.identity);
		new_newexplosion.transform.parent = transform;
		new_newexplosion.transform.localScale = new Vector3(3,3,3);

		SetBurnMaterial();
    }
    private void SetBurnMaterial(){

    	Material burn = new Material(Shader.Find("Specular"));
    	burn.SetColor("_Color",Color.black);
    	// burn.SetColor("_SpecColor", Color.black);

    	GameObject mesh_container = null;

    	try{
    		mesh_container = transform.Find("Mesh").gameObject;
    	}catch(Exception e){
    		mesh_container = transform.gameObject;
    	}

    	MeshRenderer mr = mesh_container.GetComponent<MeshRenderer>();
    	mr.material = burn;
    	
    }
}

