using UnityEngine;
using System.Collections;

public class VectorVController : VectorController {

	//
	// Velocity vector behaviour
	// Should be put on the cylinder with cone on the top
	//
	
	public override void transformVector() {
		transform.localScale = new Vector3(0.75f, projectileMotion.velocity * scale, 0.75f);
		transform.localPosition = new Vector3(projectileMotion.horizontalVelocity * scale,
									      projectileMotion.verticalVelocity * scale, 0.0f);
		transform.localEulerAngles = new Vector3(0.0f, 0.0f, -90.0f + projectileMotion.angle);
		cone.transform.localEulerAngles = new Vector3(-projectileMotion.angle, 90.0f, 90.0f);
		cone.transform.localPosition = new Vector3(transform.localPosition.x * 2.0f,
										        transform.localPosition.y * 2.0f, 0.0f);
	}
}
