using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/* The parent class extends MonoBehaviour */
public class EnemiesManager : MonoBehaviour{

	public GameObject main_character;
	public GameObject enemy;

    static int RespawnRadius = 20; //The MOBS will repawn insde this spehere
    static int AttackRadius = 10; //The MOBS will run to attack the user within this perimeter


    List<GameObject> enemies;
    List<GameObject> corpses;

    void Start() {

    	enemies = new List<GameObject>();
    	corpses = new List<GameObject>();
    }

    void Update() {

    	foreach(GameObject enemy in enemies){
    		Enemy enemy_script = enemy.GetComponent<Enemy>();
    		enemy_script.CustomUpdatePosition(main_character.transform.position, AttackRadius);
    	}

    	if(Input.GetKeyDown(KeyCode.P)){
    		CreateNewEnemy();
    	}
    }

    //Ceates the enemie and adds the enemie to the lsit
    public void CreateNewEnemy() {


    	Vector3 player_position = main_character.transform.position;

    	Vector3 random_postion = new Vector3(player_position.x + UnityEngine.Random.Range(-RespawnRadius, RespawnRadius),
    		player_position.y +  UnityEngine.Random.Range(-RespawnRadius, RespawnRadius),
                                             player_position.z + UnityEngine.Random.Range(0, 10)); //No enemies below the main character

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


    	
    }

    

    public void AttachEnemy(GameObject obs){
    	enemies.Add(obs);
    }

   //dettach the enemie and adds it to the corpses list
    public void DettachEnemy(GameObject obs) {



    }
    public void AddCorpsesList(GameObject obs){

    }

    
}