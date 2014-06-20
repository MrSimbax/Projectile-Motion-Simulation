using UnityEngine;
using System.Collections;

public class ProjectileMotion : MonoBehaviour {

	//
	// The main bullet script behaviour
	//


	// Must be provided in the inspector:
	// ----------------------------------
	public CatapultAnimationController catAnimController; // used for animation of throwing

	public Transform ballTransform;
	public Transform projectileTransform;

	public GameObject catapult;
	// public GameObject catapultBase;

	public GameObject ballBase; // a cylinder, on top of it the ball is put

	
	// Used from the outside:
	// ----------------------

	// Object to which this script is attached
	public GameObject ball {
		get { return gameObject; }
	}

	// Current velocity of the ball
	// meters per seconds
	// can be negative
	// calculated
	public float velocity {
		get { return _velocity; }
	}

	// Current horizontal velocity of the ball
	// meters per seconds
	// can be negative
	// calculated
	public float horizontalVelocity {
		get { return _horizontalVelocity; }
	}

	// Current vertical velocity of the ball
	// meters per seconds
	// can be negative
	// calculated
	public float verticalVelocity {
		get { return _verticalVelocity; }
	}

	// Current angle between the ground and the velocity vector
	// degrees
	// can be negative
	// calculated
	public float angle {
		get { return _angle; }
	}

	// Initial velocity of the ball
	// meters per seconds
	// values = <5; 100>
	// can't be changed during simulation
	public float initialVelocity {
		get { return _initialVelocity; }
		set {
			if (value < 0.0f || value > 100.0f) { return; }
			if (!_isRunning) {
				_initialVelocity = _velocity = value;
				reset();
			}
		}
	}

	// Angle between the ground and the initial velocity of the ball
	// degrees
	// values = <0; 90>
	// can't be changed during simulation
	public float launchAngle {
		get { return _launchAngle; }
		set {
			if (value < 0.0f || value > 90.0f) { return; }
			if (!_isRunning) {
				_launchAngle = _angle = value;
				_radAngle = value * Mathf.Deg2Rad;
				reset();
			}
		}
	}

	// Gravity acceleration
	// meters per square seconds
	// values = <0; 10>
	// can't be changed during simulation
	public float gravityAcceleration {
		get { return _gravityAcceleration; }
		set {
			if (value < 0.0f || value > 10.0f) { return; }
			if (!_isRunning) {
				_gravityAcceleration = value;
			}
		}
	}

	// Speed of the simulation
	// fraction (delta time will be multiplied by this value)
	// values = (0; 8>
	// can't be changed during simulation
	public float simulationSpeed {
		get { return _simulationSpeed; }
		set {
			if (value <= 0.0f || value > 8.0f) { return; }
			if (!_isRunning) {
				_simulationSpeed = value;
				reset();
			}
		}
	}

	// Height, the initial y coordinate of the ball
	// meters
	// values = <0; 10>
	// can't be changed during simulation
	public float height {
		get { return _height; }
		set {
			if (value < 0.0f || value > 100.0f) { return; }

			// if part of the ball is below ground
			if (value <= transform.localScale.y / 2.0f) {
				ballBase.renderer.enabled = false; // don't show the base
			} else {
				ballBase.renderer.enabled = true; // show it otherwise
			}

			if (!_isRunning) {
				reset(); // put ball on the base

				// Set the ball base
				float ballBaseHeight = value - 0.5f; // 0.5 is a radius of the ball
				ballBase.transform.localPosition = new Vector3(0.0f, ballBaseHeight / 2.0f, 0.0f); // center of the cylinder put in the middle
				ballBase.transform.localScale = new Vector3(1.0f, ballBaseHeight / 2.0f, 1.0f); // scale it to the proper height

				// And finally set the value
				_initialYPos = _height = value;
				reset();
			}
		}
	}

	// Time, when the ball reached the current highest height
	// seconds
	// calculated
	public float timeWhenReachedMaxHeight {
		get {
			return _timeWhenReachedMaxHeight;
		}
	}

	// Current highest height
	// meters
	// calculated
	public float maxHeight {
		get {
			return _maxHeight;
		}
	}

	// Theoretical max height calculated from the formula
	// meters
	// calculated once
	public float theoreticalMaxHeight {
		get {
			if (_gravityAcceleration == 0.0f) {
				return Mathf.Infinity;
			} else {
				return _height + Mathf.Pow(_initialVelocity * Mathf.Sin(_radAngle), 2.0f) / (2.0f * _gravityAcceleration);
			}
		}
	}

	// Current time passed from a start of the simulation
	// seconds
	// calculated
	public float time {
		get {
			return _time;
		}
	}

	// Theoretical range calculated from the formula
	// meters
	// calculated once
	public float theoreticalRange {
		get {
			if (_gravityAcceleration == 0.0f) {
				return Mathf.Infinity;
			} else {
				return (_initialVelocity * Mathf.Cos(_radAngle) / _gravityAcceleration) * (_initialVelocity * Mathf.Sin(_radAngle) + Mathf.Sqrt(Mathf.Pow(_initialVelocity * Mathf.Sin(_radAngle), 2.0f) + 2.0f * _gravityAcceleration * _height));
			}
		}
	}

	// Is trajectory showing on the screen?
	public bool isTrajectoryShowed {
		set { _isTrajectoryShowed = value; }
		get { return _isTrajectoryShowed; }
	}

	// Has simulation already started?
	public bool hasStarted {
		get { return _hasStarted; }
	}

	// Is simulation running currently?
	public bool isRunning {
		get { return _isRunning; }
	}

	// Did the ball reach ground?
	public bool isDone {
		get { return _isDone; }
	}


	// Used inside:
	// ------------

	// Initial values:
	private float _initialVelocity;
	private float _gravityAcceleration;
	private float _launchAngle;
	private float _simulationSpeed;
	private float _height;

	// Calculated values:
	private float _velocity;
	private float _horizontalVelocity;
	private float _verticalVelocity;
	private float _radLaunchAngle;
	private float _angle;
	private float _time;
	private float _xPos;
	private float _yPos;
	private float _zPos;
	private float _maxHeight;
	private float _timeWhenReachedMaxHeight;

	// Helpful values:
	private float _radAngle;
	private float _initialXPos;
	private float _initialYPos;
	private float _initialZPos;
	private TrailRenderer _trajectoryRenderer;
	private bool _hasStarted;
	private bool _isRunning;
	private bool _isDone;
	private bool _isTrailOn;
	private bool _isPrevTrailOn; // for performance reasons
	private bool _isTrajectoryShowed;

	// Methods
	// -------

	// Starts simulation
	public void start() {
		if (!_hasStarted) {
			// Don't move the catapult
			catapult.transform.parent = projectileTransform.parent;

			catAnimController.throw_();

			_hasStarted = true;
			resume();
		}
	}

	// Resume simulation after start or pause
	public void resume() {
		if (_hasStarted) {
			_isTrailOn = true;
			_isRunning = true;
		}
	}

	// Pause simulation without resetting
	public void pause() {
		if (_hasStarted) {
			_isRunning = false;
		}
	}

	// Stops and resets the simulation
	public void reset() {
		// Back to the initial position
		gameObject.transform.localPosition = new Vector3(_initialXPos, _initialYPos, _initialZPos);

		// Move catapult with the ball
		catapult.transform.parent = ball.transform;

		// Reset the catapult
		catAnimController.reset();

		// Reset the variables
		_time = 0.0f;

		_velocity = _initialVelocity;
		_horizontalVelocity = _initialVelocity * Mathf.Cos(_radLaunchAngle);
		_verticalVelocity = _initialVelocity * Mathf.Sin(_radLaunchAngle);

		_angle = _launchAngle;
		_radAngle = _radLaunchAngle;

		_maxHeight = 0.0f;
		_timeWhenReachedMaxHeight = 0.0f;

		// Reset the control variables
		_isDone = false;
		_isRunning = false;
		_hasStarted = false;
		_isTrailOn = false;
	}

	// Change the initial values to the default ones
	public void setDefaultSettings() {
		if (_isRunning) { return; }
		_initialVelocity = 50.0f;
		_gravityAcceleration = 9.81f;
		_launchAngle = _angle = 45.0f;
		_radLaunchAngle = _radAngle = _angle * Mathf.Deg2Rad;
		_simulationSpeed = 1.0f;
		_height = 10.0f;
		reset();
	}

	// Behaviour
	// ---------
	public void Start() {
		// Setup animation
		catAnimController.stretch();

		// Init variables
		_initialXPos = gameObject.transform.localPosition.x;
		_initialYPos = gameObject.transform.localPosition.y;
		_initialZPos = gameObject.transform.localPosition.z;

		setDefaultSettings();

		// Setup simulation
		_isDone = false;
		_isRunning = false;

		// Setup trail renderer
		_trajectoryRenderer = GetComponent<TrailRenderer>();
		_isTrailOn = false;
		_isPrevTrailOn = false;
		_isTrajectoryShowed = false;
	}
	
	public void Update() {
		// Trajectory
		// If simulation haven't started yet, then don't render
		if (_isTrailOn != _isPrevTrailOn) {
			if (_isTrailOn) {
				_trajectoryRenderer.time = Mathf.Infinity; // Render
				_isPrevTrailOn = true;
			} else {
				_trajectoryRenderer.time = 0.0f; // Don't render
				_isPrevTrailOn = false;
			}
		}
		// Check, if user want to show the trajectory
		if (_isTrajectoryShowed) {
			_trajectoryRenderer.endWidth = 1.0f;
			_trajectoryRenderer.startWidth = 1.0f;
		} else {
			_trajectoryRenderer.endWidth = 0.0f;
			_trajectoryRenderer.startWidth = 0.0f;
		}
	}

	public void FixedUpdate() {
		// All physics is here

		if (!_isRunning) { return; } // don't do anything if simulation's not running

		// Calculate the next position of the ball
		_xPos = _initialXPos + _initialVelocity * Mathf.Cos(_radLaunchAngle) * _time;
		_yPos = _initialYPos + _initialVelocity * Mathf.Sin(_radLaunchAngle) * _time - _gravityAcceleration * Mathf.Pow(_time, 2.0f) / 2.0f;
		_zPos = _initialZPos;

		// Calculate current velocity for the user
		_horizontalVelocity = _horizontalVelocity * 1.0f;
		_verticalVelocity = _initialVelocity * Mathf.Sin(_radLaunchAngle) - _gravityAcceleration * _time;
		_velocity = Mathf.Sqrt(Mathf.Pow(_horizontalVelocity, 2.0f) + Mathf.Pow(_verticalVelocity, 2.0f));

		// Calculate current angle for the user
		if (!Utilities.isZero(_horizontalVelocity)) {
			_angle = Mathf.Atan(_verticalVelocity / _horizontalVelocity) * Mathf.Rad2Deg;
		} else {
			if (_horizontalVelocity < 0.0f) { _angle = -90.0f; }
			else { _angle = 90.0f; }
		}

		// Calculate max height for the user
		if (_yPos > _maxHeight) {
			_maxHeight = _yPos;
			_timeWhenReachedMaxHeight = _time;
		}

		// Change the position of the ball
		if (!_isDone) {
			gameObject.transform.localPosition = new Vector3(_xPos, _yPos, _zPos);
			_time += Time.fixedDeltaTime * simulationSpeed;

			// If the ball reached the ground
			if (_yPos < 0.0f) {
				_isDone = true;
				_isRunning = false;
			}
		}
	}
}
