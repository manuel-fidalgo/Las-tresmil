using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Enemy : Character, Observer {

    public void Die() {
        throw new System.NotImplementedException();
    }

    public void SetAt(Vector3 position) {

    }

    //Interface methods
    public void Update() {
        throw new NotImplementedException();
    }
}