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

        if(dead){
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

    void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.transform.tag == "Vehicle")
		{
			float damage = 100;
			float factor = 3.5f;
			Rigidbody body = col.gameObject.GetComponent<Rigidbody>();
			float speed = body.velocity.magnitude;
			SetDamage(speed * factor);
		}
	}
}