/*using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // Must be provided in the inspector:
    public ProjectileMotion projectileMotion; // Script attached to the ball

    public Camera ballCam; // Camera focused on a ball
    public Camera fullCam; // Camera showing full trajectory

    // Used from outside:
    public bool isBallCamEnabled {
        get {
            if (ballCam != null) {
                return ballCam.enabled;
            } else {
                Debug.LogError("ballCam is null");
                Debug.Break();
                return false;
            }
        }
    }

    // Helpful variables:
    private float _range;
    private float _x;
    private float _y;
    private float _z;

    // Public methods
    public void switchCamera() {
        ballCam.enabled = !ballCam.enabled;
        fullCam.enabled = !fullCam.enabled;
    }

    public void Start() {
        // Just a random init value
        _range = -1;

        // Check if everything is ok
        if (projectileMotion == null) { Debug.LogError("projectileMotion is null"); }
        if (ballCam == null) { Debug.LogError("ballCam is null"); }
        if (fullCam == null) { Debug.LogError("fullCam is null"); }

        // Only fullCam enabled
        // Default camera
        ballCam.enabled = false;
        fullCam.enabled = true;
    }

    public void Update() {

        // When gravity equals 0 it's impossible to show full trajectory, so
        // fullCam should be disabled
        if (projectileMotion.gravityAcceleration == 0.0f) {
            fullCam.enabled = false;
            ballCam.enabled = true;
        }

        // ballCam is focused on the ball, no need to code its behaviour
        //

        // fullCam must be at proper position to show full trajectory
        if (fullCam.enabled == true) {
            _range = projectileMotion.theoreticalRange;

            _x = _range / 2; // place camera at a center
            _y = projectileMotion.theoreticalMaxHeight; // place camera at a MaxHeight

            // Calculate how far from the trajectory should be the camera
            // Choose the highest value from:
            // * range
            // * velocity
            // * max height
            _z = - Mathf.Max(_range * 1 / Mathf.Tan(60*Mathf.Deg2Rad),
                        projectileMotion.initialVelocity * 4 / Mathf.Tan(60*Mathf.Deg2Rad),
                        projectileMotion.theoreticalMaxHeight * 4 / Mathf.Tan(60*Mathf.Deg2Rad));
            
            gameObject.transform.localPosition = new Vector3(_x, _y, _z);
        }
    }
}
*/