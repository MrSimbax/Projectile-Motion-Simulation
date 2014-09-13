using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float distance = 100.0f;
    public float distanceMinLimit = 10.0f;

    public float zoomSpd = 2.0f;
    
    public float xSpeed = 240.0f;
    public float ySpeed = 123.0f;

    public int xMinLimit = 0;

    public int yMinLimit = 0;
    
    private float _x = 22.0f;
    private float _y = 33.0f;

    private float _x0;
    private float _y0;
    private float _z0;

    public void Start () {
        _x = transform.localPosition.x;
        _y = transform.localPosition.y;

        _x0 = _x;
        _y0 = _y;
        _z0 = -distance;

        camera.isOrthoGraphic = false;
    }
    
    public void LateUpdate () {
        _x += Input.GetAxis("Horizontal") * xSpeed * 0.02f;
        _y += Input.GetAxis("Vertical") * ySpeed * 0.02f;
        
        _x = Mathf.Clamp(_x, xMinLimit, Mathf.Infinity);
        _y = Mathf.Clamp(_y, yMinLimit, Mathf.Infinity);

        distance -= Input.GetAxis("Fire1") * zoomSpd * 0.02f;
        distance += Input.GetAxis("Fire2") * zoomSpd * 0.02f;

        if (!camera.isOrthoGraphic) {
            distance = Mathf.Clamp(distance, distanceMinLimit, Mathf.Infinity);
        } else {
            distance = Mathf.Clamp(distance, distanceMinLimit + 40.0f, Mathf.Infinity);
            camera.orthographicSize = Mathf.Clamp(distance - 140.0f, 10.0f, Mathf.Infinity);
        }

        transform.localPosition = new Vector3(_x, _y, -distance);
    }

    public void ResetPosition() {
        _x = _x0;
        _y = _y0;
        distance = -_z0;
        if (camera.isOrthoGraphic) {
            camera.orthographicSize = distance - 140.0f;
        }
    }

    public void SwitchIsOrtoGraphic() {
        camera.isOrthoGraphic = !camera.isOrthoGraphic;
    }
}

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