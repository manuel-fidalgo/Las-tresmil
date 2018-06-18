using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;


public class MainCharacterVR : MainCharacter{

    // Use this for initialization

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
        float angle_x = ROTATION_SPEED * Input.GetAxis("Mouse X");

        if (!dead)
        {
            DispatchOculusInput();
            RotateCharacter(angle_x);
            RotateCameraVR();
        }
        ComputeFallingDamage();
        UpdateShootingPointHandBased();
    }

    private void DispatchOculusInput()
    {
        //Axis2D.PrimaryThumbstick wasd character movement
        //Axis1D.PrimaryIndexTrigger shoot
        //button four jump
        //button tree take/leave car
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

    private void RotateCameraVR(){

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
