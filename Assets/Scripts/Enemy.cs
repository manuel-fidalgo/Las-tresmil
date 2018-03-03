using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {

    
	public NavMeshAgent agent;

     
    public override void Start(){
        base.Start();
    }

    public override void Update(){
        base.Update();
    }


    //Any enemy will be updated by the EnemiesManager
	public void CustomInizialice(){

		agent = GetComponent<NavMeshAgent>();
	}

    public void CustomUpdatePosition(Vector3 player_position, int att_radius){

        if(died){
            agent.Stop();
        }else{
       	    agent.destination = player_position;
            ManageMovement(player_position,att_radius);
        }

    }
    public void ManageMovement(Vector3 player_position, int att_radius){


    	float distance_to_target = Vector3.Distance(transform.position, player_position);

    	if(distance_to_target < att_radius){
    		agent.destination = player_position;
    	}


    	if(distance_to_target < 3){
    		Attack();
    		agent.destination = transform.position;
    	}
    	else if(agent.velocity.magnitude > 0){
    		MoveForward();
    	}else{
    		Idle();
    	}


    	ApplyAnimationParams();

    	// Debug.Log(distance_to_target + " <distance,velocity> "+ agent.velocity.magnitude);
    	
    }
    //Notify the Subject and Remove the corpse after 3 seconds.
    protected override void Die(){
        base.Die();

    }

    void OnCollisionStay(Collision collision){


         foreach (ContactPoint contact in collision.contacts) {

            Debug.DrawRay(contact.point, contact.normal, Color.blue,20f);
           if (contact.otherCollider.material.name == "Bullet (Instance)"){
                Die();
            }
        }
    }
}