﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Character : MonoBehaviour{

    protected Animator animator;
    protected Rigidbody characterRigidbody;
    protected Transform trans;


    public Dictionary<string,bool> AnimationParams;



    public void Start() {

        animator = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        characterRigidbody = GetComponent<Rigidbody>();

        AnimationParams = new Dictionary<string, bool>();
        DefineParams();
    }

    public void Update() {}


    //Seend the animation params into the  
    protected void ApplyAnimationParams(){

        foreach(var item in AnimationParams){
            animator.SetBool(item.Key,item.Value);
        }
    }

    //Set false all the animation params except the key. 
    protected void SelectAnimation(string key){

        List<string> keys = new List<string> (AnimationParams.Keys);
        
        foreach(var var_key in keys){
            AnimationParams[var_key] = false;
        }

        AnimationParams[key] = true;

    }

    private void DefineParams(){

        AnimationParams.Add("Forward",false);
        AnimationParams.Add("Backward",false);
        AnimationParams.Add("Right",false);        
        AnimationParams.Add("Left",false);
        AnimationParams.Add("Jump",false);
        AnimationParams.Add("Knee",false);
        AnimationParams.Add("Idle",true);
    }

    protected void MoveForward() {
        SelectAnimation("Forward");
    }
    protected void MoveBackward() {
        SelectAnimation("Backward");   
    }
    protected void MoveRigth() {
        SelectAnimation("Right");
    }
    protected void MoveLeft() {
        SelectAnimation("Left");
    }
    protected void Jump() {
        if(IsGrounded()){
            SelectAnimation("Jump");
            float jumpheight = 5f;
            characterRigidbody.velocity = new Vector3(0, jumpheight, 0);
        }
    }
    protected void Idle() {
        SelectAnimation("Idle");
    }

    protected void Knee(){
        SelectAnimation("Knee");
    }

    protected bool IsFalling(){

        bool ret_var = false; 
        RaycastHit hit;

        // Debug.DrawRay(transform.position,200,Color.green);

        if(Physics.Raycast(trans.position, Vector3.down,out hit,200)){

            if(hit.distance > 1.70f)
            ret_var = true;
            else
            ret_var = false;
        }else{
            ret_var = true;
        }
        // Debug.Log(ret_var);
        return ret_var;
    }

    protected bool IsGrounded(){

        bool ret_var = false; 
        RaycastHit hit;

        // Debug.DrawRay(transform.position,200,Color.green);

        if(Physics.Raycast(trans.position, Vector3.down,out hit,200)){

            if(hit.distance < 0.1f)
            ret_var = true;
            else
            ret_var = false;
        }else{
            ret_var = false;
        }

        // Debug.Log(hit.distance);
        return ret_var;
    }


}




