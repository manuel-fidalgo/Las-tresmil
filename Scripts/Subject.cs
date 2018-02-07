using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Subject {

    Observer[] list { get; set; }

    void attachObserver(Observer o) {
    }
    void dettachObserver(Observer o) {
    }

    void updateAll() {

    }
}