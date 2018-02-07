using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MainCharacter : Character {


    public List<FireArm> fire_arms;
    public static int ROTATION_SPEED = 5;
    public GameObject cameraObject;


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

       float vert = Input.GetAxis("Vertical") * 10f * Time.deltaTime;
       float hor = Input.GetAxis("Horizontal") *10f * Time.deltaTime;
       
       transform.Translate(0, 0, vert);
       transform.Translate(hor, 0, 0);

       if(vert !=0 || hor != 0){
            animator.SetBool("forward", true);
       }else{
            animator.SetBool("idle", true);
       }

    }

    //Manages actions such shoot, reload etc..
    private void FireArmAction() {

    }

}