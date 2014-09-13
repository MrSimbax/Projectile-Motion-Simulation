using UnityEngine;
using System.Collections;

public class VectorVyController : VectorController {
    
    //
    // Vertical velocity vector behaviour
    // Should be put on the cylinder with cone on the top
    //

    public override void transformVector() {
        float vy = simulationController.currentData.velocityVector.vertical;
        transform.localScale = new Vector3(0.75f, vy * scale, 0.75f);
        transform.localPosition = new Vector3(0.0f, transform.localScale.y, 0.0f);
        if (vy < 0.0f) {
            cone.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        } else {
            cone.transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
        }
        cone.transform.localPosition = new Vector3(0.0f, transform.localPosition.y * 2.0f, 0.0f);
    }
}
