using UnityEngine;
using System.Collections;

public class VectorVxController : VectorController {
	
	//
	// Horizontal velocity vector behaviour
	// Should be put on the cylinder with cone on the top
	//
	
	public override void transformVector() {
		gameObject.transform.localScale = new Vector3(0.75f, projectileMotion.horizontalVelocity * scale, 0.75f);
		gameObject.transform.localPosition = new Vector3(gameObject.transform.localScale.y, 0.0f, 0.0f);
		if (projectileMotion.horizontalVelocity < 0.0f) {
			cone.transform.localEulerAngles = new Vector3(-180.0f, 90.0f, 90.0f);
		} else {
			cone.transform.localEulerAngles = new Vector3(0.0f, 90.0f, 90.0f);
		}
		cone.transform.localPosition = new Vector3(transform.localPosition.x * 2.0f, 0.0f, 0.0f);
	}
}
