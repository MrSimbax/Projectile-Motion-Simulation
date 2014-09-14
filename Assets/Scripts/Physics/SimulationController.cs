using UnityEngine;
using System.Collections;

public class SimulationController : MonoBehaviour {

    public Transform ballTransform;

    public delegate void SimulationControlAction();
    public static event SimulationControlAction OnSimulationStart;
    public static event SimulationControlAction OnSimulationPause;
    public static event SimulationControlAction OnSimulationReset;
    public static event SimulationControlAction OnSimulationDone;

    public CatapultController catapultController;
    public BallBaseController ballBaseController;
    public TrajectoryController trajectoryController;
    // public VectorVController vectorV;
    // public VectorVxController vectorVx;
    // public Vector.VyController vectorVy;

    public ProjectileMotionData initData { get; set; }
    public ProjectileMotion projectileMotion;

    public ProjectileMotionData currentData;

    public bool hasStarted {
        get {
            return _hasStarted;
        }
    }

    public bool isRunning {
        get {
            return _isRunning;
        }
    }

    public bool isDone {
        get {
            return _isDone;
        }
    }

    private bool _hasStarted;
    private bool _isRunning;
    private bool _isDone;

    public void OnEnable() {
        initData.SomethingHasChanged += Reset;
    }

    public void OnDisable() {
        initData.SomethingHasChanged -= Reset;
    }

    public void Awake() {
        catapultController.JustReset();
        ballBaseController.height = 0.0f;
        trajectoryController.isRendering = false;
        initData = new ProjectileMotionData();
        currentData = initData;
        SetDefaultSettings();
        projectileMotion = null;
        _hasStarted = false;
        _isRunning = false;
        _isDone = false;
    }

    public void FixedUpdate() {
        if (!_isRunning) { return; }

        projectileMotion.StepToNextPosition();
        
        ballTransform.localPosition = new Vector3(projectileMotion.data.xPos,
                                                  projectileMotion.data.yPos,
                                                  projectileMotion.data.zPos);

        if (projectileMotion.data.yPos <= 0.0f) {
            _isDone = true;
            _isRunning = false;

            if (OnSimulationDone != null) {
                OnSimulationDone();
            }
        }
    }

    public void SwitchIsAirDrag(bool b) {
        initData.isAirDrag = !initData.isAirDrag;
    }

    public void SetDefaultSettings() {
        initData.yPos = 10.0f;
        initData.gravityAcceleration = 9.81f;
        initData.velocityVector.SetVector(50.0f , 45.0f);
        initData.isAirDrag = false;
        initData.mass = 1.0f;
        initData.airDensity = 1.225f;
        initData.dragCoefficient = 0.5f;
        initData.crossSectionalArea = 0.0314f;
        Reset();
    }

    public void Play() {
        if (_isRunning) {
            Debug.LogWarning("Simulation is already running, can't play it.");
        }
        if (_isDone) {
            Debug.LogWarning("Simulation is done, can't play it.");
        }

        if (!_hasStarted) {
            projectileMotion = new ProjectileMotion(initData);
            currentData = projectileMotion.data;
            catapultController.JustThrow();
            _hasStarted = true;
        }

        if (OnSimulationStart != null) {
            OnSimulationStart();
        }

        trajectoryController.isRendering = true;
        _isRunning = true;
    }

    public void Pause() {
        if (!_isRunning) {
            Debug.LogWarning("Simulation is not running.");
            return;
        }

        if (OnSimulationPause != null) {
            OnSimulationPause();
        }

        _isRunning = false;
    }

    public void Reset() {
        _isDone = false;
        _isRunning = false;
        _hasStarted = false;
        trajectoryController.isRendering = false;

        ballTransform.localPosition = new Vector3(initData.xPos,
                                                  initData.yPos,
                                                  initData.zPos);
        ballBaseController.height = initData.yPos;
        catapultController.JustReset();

        projectileMotion = null;
        currentData = initData;

        if (OnSimulationReset != null) {
            OnSimulationReset();
        }
    }
}
