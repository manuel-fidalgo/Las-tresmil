using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Subject : MonoBehaviour {

    Observer[] list { get; set; }

    void attachObserver(Observer o) {
    }
    void dettachObserver(Observer o) {
    }

    void updateAll() {

    }
}