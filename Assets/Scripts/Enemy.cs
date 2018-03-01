using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {

    
	public NavMeshAgent agent;

	public void CustomInizialice(){

		agent = GetComponent<NavMeshAgent>();
	}

    public void CustomUpdatePosition(Vector3 player_position, int att_radius){

       	agent.destination = player_position;
      	ManageMovement(player_position,att_radius);

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

    	Debug.Log(distance_to_target + " <distance,velocity> "+ agent.velocity.magnitude);
    	
    }
    //Notify the Subject and Remove the corpse after 3 seconds.
    //Enqueue into remove corpres lists
    private void DieAction(){

    }

}