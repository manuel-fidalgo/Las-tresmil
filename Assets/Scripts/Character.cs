using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Character : MonoBehaviour{

    protected Animator animator;
    protected Rigidbody characterRigidbody;
    protected Transform trans;
    public float speed;

    public string moving;

    public void Start() {
        animator = GetComponent<Animator>();
        // characterRigidbody = GetComponent<Rigidbody>();s
        trans = GetComponent<Transform>();
        
    }
    public void Update() {
    }

    protected void MoveFordward() {
    }
    protected void MoveBackward() {
    }
    protected void MoveRigth() {
    }
    protected void MoveLeft() {
    }
    protected void Jump() {
    }
    protected void Anim() {
    }
    protected void Idle() {
    }

}




