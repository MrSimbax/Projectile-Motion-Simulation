using UnityEngine;
using System.Collections;

public class VectorVController : VectorController {

    //
    // Velocity vector behaviour
    // Should be put on the cylinder with cone on the top
    //
    
    public override void transformVector() {
        float v = simulationController.currentData.velocityVector.magnitude;
        float vx = simulationController.currentData.velocityVector.horizontal;
        float vy = simulationController.currentData.velocityVector.vertical;
        float angle = simulationController.currentData.velocityVector.angle;
        transform.localScale = new Vector3(0.75f, v * scale, 0.75f);
        transform.localPosition = new Vector3(vx * scale, vy * scale, 0.0f);
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, -90.0f + angle);
        cone.transform.localEulerAngles = new Vector3(-angle, 90.0f, 90.0f);
        cone.transform.localPosition = new Vector3(transform.localPosition.x * 2.0f,
                                                transform.localPosition.y * 2.0f, 0.0f);
    }
}