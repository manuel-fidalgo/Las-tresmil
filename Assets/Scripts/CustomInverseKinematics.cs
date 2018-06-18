using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInverseKinematics : MonoBehaviour {



	public GameObject spine;
	private Animator animator;

	public Vector3 idle_offset;
	public Vector3 running_offset;

    // Use this for initialization
	void Start () {

		animator = GetComponent<Animator>();

	}
	private void LateUpdate()
	{
        MainCharacterDesktop mc = GetComponent<MainCharacterDesktop>();
		LookAt(mc.GetStraightForwardPoint());
	}

    //This method should be called from the LateUpdateMehtod
	public void LookAt(Vector3 target)
	{
       // Debug.DrawLine(spine.transform.position, target,Color.red,20);
		
		if(!animator.GetBool("Driving")){
			spine.transform.LookAt(target);

			if(animator.GetBool("Forward")){
				spine.transform.rotation = spine.transform.rotation * Quaternion.Euler(running_offset);
			}else{
				spine.transform.rotation = spine.transform.rotation * Quaternion.Euler(idle_offset);
			}
		}
	}
}