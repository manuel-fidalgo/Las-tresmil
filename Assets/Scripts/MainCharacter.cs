using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacter : Character {


	public List<GameObject> fire_arms;
	public int weapon_in_use;

	public static int ROTATION_SPEED = 5;
	private GameObject cameraObject;

	public static float FORWARD_SPEED = 9f;
	public static float LATERALBACKWARD_SPEED = 5f;
  private float falling_time = 0;

    // EnemiesManager enemies;



  public override void Start(){
    base.Start();
    Cursor.visible = false;
    cameraObject = transform.Find("Camera").gameObject;

  }

  public override void Update(){

    base.Update();

    float angle_x = ROTATION_SPEED * Input.GetAxis("Mouse X");
    float angle_y = ROTATION_SPEED * Input.GetAxis("Mouse Y");

    if(!dead){
      DispatchKeyInput();
      RotateCharacter(angle_x);
      RotateCamera(angle_y);
    }
    ComputeFallingDamage();
    UpdateShootingPointInFireArm();

  }
  public void ComputeFallingDamage(){

    if(IsGrounded()){
      if(falling_time > 5){
        Debug.Log("Fall with damage, time->" + falling_time);
        SetDamage(falling_time * 2);
      }
      falling_time = 0;
    }else{
     falling_time = falling_time + Time.deltaTime;
   }
 }

 private void UpdateShootingPointInFireArm(){

  GameObject weapon = fire_arms[weapon_in_use];
  FireArm controller = weapon.GetComponent<FireArm>();
  controller.UpdateShootingPoint(getShootingpoint());
}


    //Get the point where the player is pointing with the crosshair.
public Vector3 getShootingpoint(){
  Camera camera = cameraObject.GetComponent<Camera>();
  Ray camera_ray = camera.ViewportPointToRay(new Vector3(0.5f,0.5f,0.5f));
  int infinite_distance = 1000;

  RaycastHit hit;
  Vector3 shooting_point;
        // Debug.DrawRay(camera_ray.origin, camera_ray.direction * infinite_distance, Color.white, 2.0f);

  bool collision = Physics.Raycast(camera_ray, out hit);

  if(collision){
            //The lines will cross in the hit point
    shooting_point = hit.point;
  }else{
            //The lines will cross in the "infinte"
    shooting_point = camera_ray.origin + (camera_ray.direction.normalized * infinite_distance);
  }
        // Debug.DrawLine(camera_ray.origin, shooting_point, Color.magenta, 2.0f);
  return shooting_point;
}

        //Get the point where the player is pointing with the crosshair.

    //Used in inverse Knematics, return a point in the cross hair direction.
public Vector3 getStraightforwardpoint(){

  Camera camera = cameraObject.GetComponent<Camera>();
  Ray camera_ray = camera.ViewportPointToRay(new Vector3(0.5f,0.5f,0.5f));
  int infinite_distance = 1000;

  Vector3 shooting_point;
  shooting_point = camera_ray.origin + (camera_ray.direction.normalized * infinite_distance);

  return shooting_point;
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


        }else{ //Camera goes down

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

        //Todo --> Move this to the main character class


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

      if(Input.GetKeyDown(KeyCode.E))
      TakeVehicle();

      if(Input.GetKey(KeyCode.Escape))
      BackToMenu();


      ApplyAnimationParams();



    }

    private void TakeVehicle(){


      Camera camera = cameraObject.GetComponent<Camera>();
      Ray camera_ray = camera.ViewportPointToRay(new Vector3(0.5f,0.5f,0.5f));

      RaycastHit hit;
      bool collision = Physics.Raycast(camera_ray, out hit);

      if(collision && (hit.collider.gameObject.tag == "Vehicle") && hit.distance < 7){

        GameObject vehicle = hit.collider.gameObject;
        VehicleController controller = vehicle.GetComponent<VehicleController>();
        MainCharacter character = this.GetComponent<MainCharacter>();


        controller.enabled = true;
        character.enabled = false;

        transform.parent = vehicle.transform;
        transform.position = vehicle.transform.Find("DriverPosition").position;
        transform.rotation = vehicle.transform.Find("DriverPosition").rotation;

        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic  = true;

            //Swaping cameras
        cameraObject.SetActive(false);
        vehicle.transform.Find("Camera").gameObject.SetActive(true);

        SetPistol();
        Drive();
      }
    }

    // This method will be called by the car.
    // @param -> The vehicle wich calls the method
    public void LeaveVehicle(GameObject vehicle){

      Debug.Log("Leaving car.");

      vehicle.GetComponent<VehicleController>().enabled = false;
      MainCharacter character = this.GetComponent<MainCharacter>();

      character.enabled = true;
      GetComponent<CapsuleCollider>().enabled = true;
      GetComponent<Rigidbody>().isKinematic  = false;

      transform.parent = null;   
      transform.position = vehicle.transform.position - vehicle.transform.right;
      transform.eulerAngles = new Vector3(0,0,0);

      cameraObject.SetActive(true);
      Idle();
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

  private void SetPistol(){

    fire_arms[weapon_in_use].SetActive(false);
    weapon_in_use = 0;
    fire_arms[weapon_in_use].SetActive(true);
    TriggerAnimation("Pistol");

  }

  protected override void Die(){

    base.Die();

    CustomInverseKinematics ik = GetComponent<CustomInverseKinematics>();
    ik.enabled = false;

    Invoke("BackToMenu", 4);
  }

  public void BackToMenu(){
    Cursor.visible = true;
    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
  }

}