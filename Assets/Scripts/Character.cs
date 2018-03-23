using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Character : MonoBehaviour{

    protected Animator animator;
    protected Rigidbody characterRigidbody;
    protected Transform trans;

    protected int Health;

    public Dictionary<string,bool> AnimationParams;
    protected bool dead = false;



    public virtual void Start() {

        Health = 100;

        animator = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        characterRigidbody = GetComponent<Rigidbody>();

        AnimationParams = new Dictionary<string, bool>();
        DefineParams();
    }

    public virtual void Update() {

    }

    public void SetDamage(float amount){
        Debug.Log("Damage->"+amount);
        Health = Health - (int) amount;
        if(Health <= 0){
            Die();
        }
    }

    protected virtual void Die(){
        
        if(!dead)
            TriggerAnimation("Die");
        dead = true;
    }


    //Send the animation params into the  
    protected void ApplyAnimationParams(){

        foreach(var item in AnimationParams){
            animator.SetBool(item.Key,item.Value);
        }
    }
    protected void TriggerAnimation(string anim){
        animator.SetTrigger(anim);
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
        AnimationParams.Add("Idle",true);

        AnimationParams.Add("Backward",false);
        AnimationParams.Add("Right",false);        
        AnimationParams.Add("Left",false);
        AnimationParams.Add("Jump",false);
        AnimationParams.Add("Knee",false);


        AnimationParams.Add("Attacking",false);
        AnimationParams.Add("Driving",false);
    }

    protected void Drive(){
         SelectAnimation("Driving");
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

    protected void Attack(){
        SelectAnimation("Attacking");
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
    public bool IsDead(){
        return dead;
    }

}




