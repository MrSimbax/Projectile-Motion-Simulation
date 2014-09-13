using UnityEngine;
using System.Collections;

public class VectorVxController : VectorController {
    
    //
    // Horizontal velocity vector behaviour
    // Should be put on the cylinder with cone on the top
    //
    
    public override void transformVector() {
        float vx = simulationController.currentData.velocityVector.horizontal;
        transform.localScale = new Vector3(0.75f, vx * scale, 0.75f);
        transform.localPosition = new Vector3(transform.localScale.y, 0.0f, 0.0f);
        if (vx < 0.0f) {
            cone.transform.localEulerAngles = new Vector3(-180.0f, 90.0f, 90.0f);
        } else {
            cone.transform.localEulerAngles = new Vector3(0.0f, 90.0f, 90.0f);
        }
        cone.transform.localPosition = new Vector3(transform.localPosition.x * 2.0f, 0.0f, 0.0f);
    }
}