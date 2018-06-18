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
    protected GameObject cameraObject;

    public static float FORWARD_SPEED = 9f;
    public static float LATERALBACKWARD_SPEED = 5f;
    protected float falling_time = 0;


    public override void Start(){
        base.Start();
    }

    public override void Update(){
        base.Update();
    }

    protected void RotateCharacter(float angle) {
        trans.Rotate(trans.up, angle);
    }

    public void ComputeFallingDamage(){

        if (IsGrounded())
        {
            if (falling_time > 5)
            {
                Debug.Log("Fall with damage, time->" + falling_time);
                SetDamage(falling_time * 2);
            }
            falling_time = 0;
        }
        else
        {
            falling_time = falling_time + Time.deltaTime;
        }
    }

    protected void TakeVehicle(){

        Camera camera = cameraObject.GetComponent<Camera>();
        Ray camera_ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        RaycastHit hit;
        bool collision = Physics.Raycast(camera_ray, out hit);

        if (collision && (hit.collider.gameObject.tag == "Vehicle") && hit.distance < 7)
        {

            GameObject vehicle = hit.collider.gameObject;
            VehicleController controller = vehicle.GetComponent<VehicleController>();
            MainCharacter character = this.GetComponent<MainCharacter>();


            controller.enabled = true;
            character.enabled = false;

            transform.parent = vehicle.transform;
            transform.position = vehicle.transform.Find("DriverPosition").position;
            transform.rotation = vehicle.transform.Find("DriverPosition").rotation;

            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            //Swaping cameras
            cameraObject.SetActive(false);
            vehicle.transform.Find("Camera").gameObject.SetActive(true);

            SetPistol();
            Drive();
        }
    }

    // This method will be called by the car.
    // @param -> The vehicle wich calls the method
    public void LeaveVehicle(GameObject vehicle)
    {

        Debug.Log("Leaving car.");

        vehicle.GetComponent<VehicleController>().enabled = false;
        MainCharacter character = this.GetComponent<MainCharacter>();

        character.enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

        transform.parent = null;
        transform.position = vehicle.transform.position - vehicle.transform.right;
        transform.eulerAngles = new Vector3(0, 0, 0);

        cameraObject.SetActive(true);
        Idle();
    }

    protected void ChangeWeapon()
    {

        fire_arms[weapon_in_use].SetActive(false);
        weapon_in_use++;
        if (weapon_in_use >= 4)
        {
            weapon_in_use = 0;
        }
        fire_arms[weapon_in_use].SetActive(true);

        if (weapon_in_use == 0)
        {
            TriggerAnimation("Pistol");
        }
        else
        {
            TriggerAnimation("Rifle");
        }
    }

    protected void SetPistol()
    {

        fire_arms[weapon_in_use].SetActive(false);
        weapon_in_use = 0;
        fire_arms[weapon_in_use].SetActive(true);
        TriggerAnimation("Pistol");

    }

    public void BackToMenu() {
        Cursor.visible = true;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }


}
