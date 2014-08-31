/*using UnityEngine;
using System.Collections;

public abstract class VectorController : MonoBehaviour {

	//
	// Vector behaviour
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
		
		if (!_isShowed || Utilities.isZero(projectileMotion.velocityVector.magnitude)) {
			renderer.enabled = false;
			cone.renderer.enabled = false;
		} else {
			renderer.enabled = true;
			cone.renderer.enabled = true;
		}

		transformVector();
	}

	public abstract void transformVector();
}*/
