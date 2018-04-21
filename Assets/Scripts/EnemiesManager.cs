using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/* The parent class extends MonoBehaviour */
public class EnemiesManager : MonoBehaviour {

	public GameObject main_character;
	public GameObject enemy;

	public static int LEVEL; //range(1,30)
	public static int MAX_LEVEL = 30;

    static int RespawnRadius = 20; //The MOBS will repawn insde this spehere
    static int AttackRadius = 10; //The MOBS will run to attack the user within this perimeter

    List<GameObject> enemies;

    private float Acc;
    private float Chrono;

    void Start() {

    	Acc = 0;
    	Chrono = 0;
    	enemies = new List<GameObject>();
    }

    void FixedUpdate() {

    	//Backwards iteration over the enemies list
    	for(int i = enemies.Count -1 ; i >= 0; i--){

    		GameObject enemy = enemies[i];
    		Enemy enemy_script = enemy.GetComponent<Enemy>();

    		//If it's dead remove the gameobject from the list and destroy the object after 5 seconds.
    		if(enemy_script.IsDead()){
    			enemies.RemoveAt(i);
    			Destroy(enemy,5);
    		}
    		enemy_script.CustomUpdatePosition(main_character.transform.position, AttackRadius);
    	}

    	UpdateChronos();

        // Debug.Log("Enemies->"+enemies.Count);
        //No more than 6 enemies in scene due to performance
    	if( (Acc > CalculateRespawnTime()) && (enemies.Count <= 6) ){
    		CreateNewEnemy();
    		Acc = 0;
    	}

    }
    private void UpdateChronos(){
    	Acc =  Acc + Time.deltaTime;
    }

    //Returns the remaining time to respawn the next enemy
    //TODO -> Implement a cool algorithm 
    private float CalculateRespawnTime(){

        int ret = 0;

        if(LEVEL < 10){
            ret = 15;
        }else if(LEVEL < 20){
            ret = 10;
        }else{
            ret = 5;
        }

    	return ret;
    }

    //Ceates the enemie and adds the enemie to the lsit
    public void CreateNewEnemy() {


    	Vector3 player_position = main_character.transform.position;
        
        //No enemies below the main character
    	Vector3 random_postion = new Vector3(player_position.x + UnityEngine.Random.Range(-RespawnRadius, RespawnRadius),
    	player_position.y +  UnityEngine.Random.Range(-RespawnRadius, RespawnRadius), player_position.z + UnityEngine.Random.Range(0, 10)); 

    	RaycastHit hit;
    	Vector3 enemy_position = Vector3.forward;

    	bool hit_success = Physics.Raycast(random_postion, -Vector3.up, out hit, 100.0f);
    	if(hit_success){
    		enemy_position = hit.point;
    		GameObject current_enemy = Instantiate(enemy, enemy_position , Quaternion.identity);
    		Enemy enemy_script = current_enemy.GetComponent<Enemy>();
    		enemy_script.CustomInizialice();
    		AttachEnemy(current_enemy);
    	}

        if(LEVEL < 30) LEVEL++;
    	
    }

    public void AttachEnemy(GameObject obs){
    	enemies.Add(obs);
    }

}