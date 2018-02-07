using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour {



    public static int ROTATION_SPEED = 5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        SetRotation(Input.GetAxis("Mouse X"));
	}

    void SetRotation(float rotation_input) {

        Transform _object;
        float angle_x = 0;

        _object = gameObject.transform.root;
        angle_x = ROTATION_SPEED * rotation_input;

        gameObject.transform.RotateAround(_object.position, _object.transform.up, angle_x);
    }
}
