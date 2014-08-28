using UnityEngine;
using System.Collections;

public class ProjectileMotion : MonoBehaviour {

    //
    // The main bullet script behaviour
    //


    // Must be provided in the inspector:
    // ----------------------------------
    // used for animation of throwing
    public CatapultController catapultController;
    public BallBaseController ballBaseController;
    public TrajectoryController trajectoryController;

    
    // Used from the outside:
    // ----------------------

    public GameObject ball {
        get { return gameObject; }
    }

    // Current velocity vector of the ball
    // All data is in meters per seconds
    // calculated
    public VelocityVector velocityVector {
        get { return _velocityVector; }
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
                _initialVelocity = value;
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
                _launchAngle = value;
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
            if (value < 0.0f) {
                Debug.LogError("ProjectileMotion: Height can't be less than zero.");
                return;
            }

            if (!_hasStarted) {
                ballBaseController.height = value;
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
                return _height +
                       Mathf.Pow(_initialVelocity * Mathf.Sin(_launchAngle * Mathf.Deg2Rad), 2.0f) /
                       (2.0f * _gravityAcceleration);
            }
        }
    }

    // Current time passed from the start of the simulation
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
                return ((_initialVelocity *
                    Mathf.Cos(_launchAngle * Mathf.Deg2Rad) / _gravityAcceleration) * (
                    _initialVelocity * Mathf.Sin(_launchAngle * Mathf.Deg2Rad) +
                    Mathf.Sqrt(
                        Mathf.Pow(_initialVelocity * Mathf.Sin(_launchAngle * Mathf.Deg2Rad), 2.0f)+
                        2.0f * _gravityAcceleration * _height)
                    ));
            }
        }
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
    protected VelocityVector _velocityVector;
    protected float _time;
    protected float _deltaTime;
    protected float _xPos;
    protected float _yPos;
    protected float _zPos;
    protected float _maxHeight;
    protected float _timeWhenReachedMaxHeight;

    // Helpful values:
    protected float _initialXPos;
    protected float _initialYPos;
    protected float _initialZPos;
    protected bool _hasStarted;
    protected bool _isRunning;
    protected bool _isDone;

    // Methods
    // -------

    // Starts simulation
    public void start() {
        if (!_hasStarted) {
            catapultController.JustThrow();

            _hasStarted = true;
            resume();
        }
    }

    // Resume simulation after start or pause
    public void resume() {
        if (_hasStarted) {
            trajectoryController.isRendering = true;
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

    private void resetTime() {
        _time = 0.0f;
        _deltaTime = Time.fixedDeltaTime * simulationSpeed;
    }

    private void resetVelocity() {
        _velocityVector.SetVector(_initialVelocity, _launchAngle);
    }

    private void resetMaxHeight() {
        _maxHeight = 0.0f;
        _timeWhenReachedMaxHeight = 0.0f;
    }

    private void resetSimulation() {
        _isDone = false;
        _isRunning = false;
        _hasStarted = false;
        trajectoryController.isRendering = false;
    }

    // Stops and resets the simulation
    public void reset() {
        backToInitialPosition();
        catapultController.JustReset();

        resetTime();
        resetVelocity();
        resetMaxHeight();

        resetSimulation();
    }

    // Change the initial values to the default ones
    public virtual void setDefaultSettings() {
        if (_isRunning) { return; }
        _initialVelocity = 50.0f;
        _gravityAcceleration = 9.81f;
        _launchAngle = 45.0f;
        _simulationSpeed = 1.0f;
        _height = 10.0f;
        reset();
    }

    // Behaviour
    // ---------

    private void initPosition() {
        _initialXPos = gameObject.transform.localPosition.x;
        _initialYPos = gameObject.transform.localPosition.y;
        _initialZPos = gameObject.transform.localPosition.z;
    }

    private void setupSimulation() {
        _isDone = false;
        _isRunning = false;
    }

    private void initObjects() {
        _velocityVector = new VelocityVector();
    }

    public void Start() {
        initObjects();
        initPosition();
        setDefaultSettings();
        setupSimulation();
    }

    private void calculateNextPosition() {
        _xPos += _velocityVector.horizontal * _deltaTime;
        _yPos += _velocityVector.vertical * _deltaTime;
        _zPos += 0.0f;
    }

    protected virtual void calculateCurrentVelocity() {
        Debug.Log("without air drag");
        float horizontal = 0.0f;
        float vertical = - _gravityAcceleration * _deltaTime;
        _velocityVector.UpdateVector(horizontal, vertical);
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
        calculateMaxHeight();

        changePosition();
        updateTime();
    }
}
