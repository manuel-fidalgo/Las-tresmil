using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/* The parent class extends MonoBehaviour */
public class EnemiesManager : Subject{

    public List<Observer> observers;

    public void attachObserver() {
        throw new NotImplementedException();
    }

    public void dettachObserver() {
        throw new NotImplementedException();
    }
}