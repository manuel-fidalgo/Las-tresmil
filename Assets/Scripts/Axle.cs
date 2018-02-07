using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Axle {

    public Wheel [] wheels;
    
    public Wheel RightWheel() {
        return wheels[0];
    }
    public Wheel LeftWheel() {
        return wheels[1];
    }
    public void WheelPhysic() {

    }
    public void WheelsAnimation() {

    }
    public void AntiRollForces() {

    }
}