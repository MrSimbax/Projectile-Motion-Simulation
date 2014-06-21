using UnityEngine;
using System.Collections;

public class VectorVxController : MonoBehaviour {
	
	//
	// Horizontal velocity vector behaviour
	// Should be put on the cylinder with cone on the top
	//
	
	public ProjectileMotion projectileMotion;
	public GameObject cone;
	public float scale = 1.0f;
	
	public bool isShowed {
		get { return _isShowed; }
		set { _isShowed = value; }
	}
	
	private bool _isShowed;
	
	public void Update () {
		
		if (!_isShowed || Utilities.isZero(projectileMotion.velocity)) {
			gameObject.renderer.enabled = false;
			cone.renderer.enabled = false;
		} else {
			gameObject.renderer.enabled = true;
			cone.renderer.enabled = true;
		}
		
		// Magic
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
