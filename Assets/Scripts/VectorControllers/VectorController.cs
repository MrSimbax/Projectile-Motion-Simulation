using UnityEngine;
using System.Collections;

public abstract class VectorController : MonoBehaviour {

	//
	// Vector behaviour
	// Should be put on the cylinder with cone on the top
	//
	
	public SimulationController simulationController;
	public GameObject cone;
	public float scale = 1.0f;
	
	public bool isShowed {
		get { return _isShowed; }
		set { _isShowed = value; }
	}

    public void SwitchIsShowed() {
        _isShowed = !_isShowed;
    }
	
	private bool _isShowed;
	
	public void Update () {
		
		if (!_isShowed || Utilities.isZero(simulationController.currentData.velocityVector.magnitude)) {
			renderer.enabled = false;
			cone.renderer.enabled = false;
		} else {
			renderer.enabled = true;
			cone.renderer.enabled = true;
		}

		transformVector();
	}

	public abstract void transformVector();
}
