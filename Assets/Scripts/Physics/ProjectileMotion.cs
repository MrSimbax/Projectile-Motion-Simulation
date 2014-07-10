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

	// Object which this script is attached to
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
			if (value < 0.0f) { return; }
			if (!_hasStarted) {
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
			if (!_hasStarted) {
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
			if (value < 0.0f) { return; }
			if (!_hasStarted) {
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
			if (value <= 0.0f) { return; }
			if (!_hasStarted) {
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
			if (value < 0.0f) { return; }

			// if part of the ball is below ground
			if (value <= transform.localScale.y / 2.0f) {
				ballBase.renderer.enabled = false; // don't show the base
			} else {
				ballBase.renderer.enabled = true; // show it otherwise
			}

			if (!_hasStarted) {
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

	// Physics interval
	// seconds
	// calculated (based on Time.fixedDeltaTime and simulationSpeed)
	public float deltaTime {
		get {
			return _deltaTime;
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
	protected float _initialVelocity;
	protected float _gravityAcceleration;
	protected float _launchAngle;
	protected float _simulationSpeed;
	protected float _height;

	// Calculated values:
	protected float _velocity;
	protected float _horizontalVelocity;
	protected float _verticalVelocity;
	protected float _radLaunchAngle;
	protected float _angle;
	protected float _time;
	protected float _deltaTime;
	protected float _xPos;
	protected float _yPos;
	protected float _zPos;
	protected float _maxHeight;
	protected float _timeWhenReachedMaxHeight;

	// Helpful values:
	protected float _radAngle;
	protected float _initialXPos;
	protected float _initialYPos;
	protected float _initialZPos;
	protected TrailRenderer _trajectoryRenderer;
	protected bool _hasStarted;
	protected bool _isRunning;
	protected bool _isDone;
	protected bool _isTrailOn;
	protected bool _isPrevTrailOn; // for performance reasons
	protected bool _isTrajectoryShowed;

	// Methods
	// -------

	private void setNotMovingCatapultWithBall() {
		catapult.transform.parent = projectileTransform.parent;
	}

	// Starts simulation
	public void start() {
		if (!_hasStarted) {
			setNotMovingCatapultWithBall();

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

	private void backToInitialPosition() {
		_xPos = _initialXPos;
		_yPos = _initialYPos;
		_zPos = _initialZPos;
		gameObject.transform.localPosition = new Vector3(_xPos, _yPos, _zPos);
	}

	private void setMovingCatapultWithBall() {
		catapult.transform.parent = ball.transform;
	}

	private void resetCatapult() {
		catAnimController.reset();
	}

	private void resetTime() {
		_time = 0.0f;
		_deltaTime = Time.fixedDeltaTime * simulationSpeed;
	}

	private void resetVelocity() {
		_velocity = _initialVelocity;
		_horizontalVelocity = _initialVelocity * Mathf.Cos(_radLaunchAngle);
		_verticalVelocity = _initialVelocity * Mathf.Sin(_radLaunchAngle);
	}

	private void resetAngle() {
		_angle = _launchAngle;
		_radAngle = _radLaunchAngle;
	}

	private void resetMaxHeight() {
		_maxHeight = 0.0f;
		_timeWhenReachedMaxHeight = 0.0f;
	}

	private void resetSimulation() {
		_isDone = false;
		_isRunning = false;
		_hasStarted = false;
		_isTrailOn = false;
	}

	// Stops and resets the simulation
	public void reset() {
		backToInitialPosition();
		setMovingCatapultWithBall();
		resetCatapult();

		resetTime();
		resetAngle();
		resetVelocity();
		resetMaxHeight();

		resetSimulation();
	}

	// Change the initial values to the default ones
	public virtual void setDefaultSettings() {
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
	private void setupAnimation() {
		catAnimController.stretch();
	}

	private void initPosition() {
		_initialXPos = gameObject.transform.localPosition.x;
		_initialYPos = gameObject.transform.localPosition.y;
		_initialZPos = gameObject.transform.localPosition.z;
	}

	private void setupSimulation() {
		_isDone = false;
		_isRunning = false;
	}

	private void setupTrailRenderer() {
		_trajectoryRenderer = GetComponent<TrailRenderer>();
		_isTrailOn = false;
		_isPrevTrailOn = false;
		_isTrajectoryShowed = false;
	}

	public void Start() {
		setupAnimation();
		initPosition();
		setDefaultSettings();
		setupSimulation();
		setupTrailRenderer();
	}

	private void renderTrajectory() {
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

	public void Update() {
		renderTrajectory();
	}

	private void calculateNextPosition() {
		_xPos += _horizontalVelocity * _deltaTime;
		_yPos += _verticalVelocity * _deltaTime;
		_zPos += 0;
	}

	protected virtual void calculateCurrentVelocity() {
		Debug.Log("without air drag");
		_horizontalVelocity += 0;
		_verticalVelocity += - _gravityAcceleration * _deltaTime;
		_velocity = Mathf.Sqrt(Mathf.Pow(_horizontalVelocity, 2.0f) + Mathf.Pow(_verticalVelocity, 2.0f));
	}

	private void calculateCurrentAngle() {
		if (!Utilities.isZero(_horizontalVelocity)) {
			_angle = Mathf.Atan(_verticalVelocity / _horizontalVelocity) * Mathf.Rad2Deg;
		} else {
			if (_horizontalVelocity < 0.0f) { _angle = -90.0f; }
			else { _angle = 90.0f; }
		}
	}

	private void calculateMaxHeight() {
		if (_yPos > _maxHeight) {
			_maxHeight = _yPos;
			_timeWhenReachedMaxHeight = _time;
		}
	}

	private void changePosition() {
		if (!_isDone) {
			gameObject.transform.localPosition = new Vector3(_xPos, _yPos, _zPos);
			
			// If the ball reached the ground
			if (_yPos < 0.0f) {
				_isDone = true;
				_isRunning = false;
			}
		}
	}

	private void updateTime() {
		if (!_isDone) {
			_time += _deltaTime;
		}
	}

	public void FixedUpdate() {
		// All physics is here
		if (!_isRunning) { return; } // don't do anything if simulation's not running

		// Physics
		calculateCurrentVelocity();
		calculateNextPosition();

		// Info for user
		calculateCurrentAngle();
		calculateMaxHeight();

		changePosition();
		updateTime();
	}
}
