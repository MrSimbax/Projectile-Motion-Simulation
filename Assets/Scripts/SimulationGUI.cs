using UnityEngine;
using System.Collections;

public class SimulationGUI : MonoBehaviour {

	//
	// All GUI is implemented here
	// Attach it to an empty object
	//	

	// Inspector
	public ProjectileMotion projectileMotion;

	public CameraController cameraController;

	public VectorVxController vectorVxController;
	public VectorVyController vectorVyController;
	public VectorVController vectorVController;

	// Helpful variables
	private string _pauseButtonString;

	// Windows

	// Controls window
	private int _controlsWindowHeight;
	private int _controlsWindowWidth;
	private int _controlsWindowLeft;
	private int _controlsWindowTop;
	private Rect _controlsWindowRect;

	// Values window
	private int _valuesWindowHeight;
	private int _valuesWindowWidth;
	private int _valuesWindowLeft;
	private int _valuesWindowTop;
	private Rect _valuesWindowRect;
	// Values window - slider values
	private float _initialVelocity;
	private float _launchAngle;
	private float _gravityAcceleration;
	private float _simulationSpeed;
	private float _height;
	// Values window - text field strings
	private string _initialVelocityString;
	private string _launchAngleString;
	private string _gravityAccelerationString;
	private string _simulationSpeedString;
	private string _heightString;
	private int _valuesTextFieldsWidth;

	// About window
	private bool _isAboutWindowShowed;
	private int _aboutWindowHeight;
	private int _aboutWindowWidth;
	private int _aboutWindowLeft;
	private int _aboutWindowTop;
	private Rect _aboutWindowRect;

	// Width of the text boxes
	private int _boxesWidth;

	// Checkbox values
	private bool _isBallCameraEnabled;
	private bool _isAirResistance;
	private bool _isVectorVxShowed;
	private bool _isVectorVyShowed;
	private bool _isVectorVShowed;
	private bool _isTrajectoryShowed;
	
	public void Start() {
		_initialVelocity = projectileMotion.initialVelocity;
		_launchAngle = projectileMotion.launchAngle;
		_gravityAcceleration = projectileMotion.gravityAcceleration;
		_simulationSpeed = projectileMotion.simulationSpeed;
		_height = projectileMotion.height;
				
		_initialVelocityString = Utilities.Round(_initialVelocity);
		_launchAngleString = Utilities.Round(_launchAngle);
		_gravityAccelerationString = Utilities.Round(_gravityAcceleration);
		_simulationSpeedString = Utilities.Round(_simulationSpeed);
		_heightString = Utilities.Round(_height);
				
		_isBallCameraEnabled = cameraController.isBallCamEnabled;
		_isAirResistance = false;
		_isVectorVxShowed = false;
		_isVectorVyShowed = false;
		_isVectorVShowed = false;
		_isTrajectoryShowed = false;

		_pauseButtonString = "Pause";

		_controlsWindowHeight = 50;
		_controlsWindowWidth = Screen.width;
		_controlsWindowLeft = 0;
		_controlsWindowTop = Screen.height - _controlsWindowHeight;
		_controlsWindowRect = new Rect(_controlsWindowLeft, _controlsWindowTop, _controlsWindowWidth, _controlsWindowHeight);

		_valuesWindowHeight = 200;
		_valuesWindowWidth = Screen.width / 2;
		_valuesWindowLeft = Screen.width / 2;
		_valuesWindowTop = 0;
		_valuesWindowRect = new Rect(_valuesWindowLeft, _valuesWindowTop, _valuesWindowWidth, _valuesWindowHeight);

		_initialVelocityString = Utilities.Round(projectileMotion.initialVelocity);
		_launchAngleString = Utilities.Round(projectileMotion.launchAngle);
		_gravityAccelerationString = Utilities.Round(projectileMotion.gravityAcceleration);
		_simulationSpeedString = Utilities.Round(projectileMotion.simulationSpeed);
		_heightString = Utilities.Round(projectileMotion.height);

		_valuesTextFieldsWidth = 45;

		_isAboutWindowShowed = false;
		_aboutWindowHeight = 300;
		_aboutWindowWidth = 300;
		_aboutWindowLeft = (Screen.width - _aboutWindowWidth) / 2;
		_aboutWindowTop = (Screen.height - _aboutWindowHeight) / 2;
		_aboutWindowRect = new Rect(_aboutWindowLeft, _aboutWindowTop, _aboutWindowWidth, _aboutWindowHeight);

		 _boxesWidth = 200;
	}

	public void OnGUI() {
		_controlsWindowRect = GUI.Window(0, _controlsWindowRect, doControlsWindow, "Controls");
		_valuesWindowRect = GUI.Window(1, _valuesWindowRect, doValuesWindow, "Values (can't be changed during simulation)");
		if (_isAboutWindowShowed) {
			_aboutWindowRect = GUI.Window(2, _aboutWindowRect, doAboutWindow, "About");
		}
	}

	private void doControlsWindow(int id) {
		GUILayout.BeginArea(new Rect(5, 20, _controlsWindowWidth - 10, _controlsWindowHeight - 5));
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Start")) { projectileMotion.start(); }
		if (!projectileMotion.hasStarted) _pauseButtonString = "Pause";
		if (GUILayout.Button(_pauseButtonString)) {
			if (_pauseButtonString == "Pause" && projectileMotion.hasStarted) {
				projectileMotion.pause();
				_pauseButtonString = "Resume";
			} else if (_pauseButtonString == "Resume") {
				projectileMotion.resume();
				_pauseButtonString = "Pause";
			}
		}
		if (GUILayout.Button("Reset")) { projectileMotion.reset(); _pauseButtonString = "Pause"; }
		if (GUILayout.Button("Default")) { projectileMotion.setDefaultSettings(); _pauseButtonString = "Pause"; Start(); }
		if (GUILayout.Button("About")) { _isAboutWindowShowed = !_isAboutWindowShowed; }
		if (GUILayout.Button("Quit")) { Application.Quit(); }
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	private void doValuesWindow(int id) {
		float newValue;

		GUILayout.BeginArea(new Rect(5, 20, _valuesWindowWidth - 10, _valuesWindowHeight - 5));
		GUILayout.BeginVertical();

		// Initial velocity
		GUILayout.BeginHorizontal();
		GUILayout.Box("Initial Velocity [m/s]", GUILayout.Width(_boxesWidth));
		newValue = GUILayout.HorizontalSlider(_initialVelocity, 0.0f, 100.0f);
		_initialVelocityString = GUILayout.TextField(_initialVelocityString, 6, GUILayout.Width(_valuesTextFieldsWidth));
		changeTextFieldAndSliderValue("initialVelocity", ref _initialVelocity, ref _initialVelocityString, newValue);
		GUILayout.EndHorizontal();

		// Launch angle
		GUILayout.BeginHorizontal();
		GUILayout.Box("Launch Angle [deg]", GUILayout.Width(_boxesWidth));
		newValue = GUILayout.HorizontalSlider(_launchAngle, 0.0f, 90.0f);
		_launchAngleString = GUILayout.TextField(_launchAngleString, 6, GUILayout.Width(_valuesTextFieldsWidth));
		changeTextFieldAndSliderValue("launchAngle", ref _launchAngle, ref _launchAngleString, newValue);
		GUILayout.EndHorizontal();

		// Gravity Acceleration
		GUILayout.BeginHorizontal();
		GUILayout.Box("Gravity Acceleration [m/s^2]", GUILayout.Width(_boxesWidth));
		newValue = GUILayout.HorizontalSlider(_gravityAcceleration, 0.0f, 10.0f);
		_gravityAccelerationString = GUILayout.TextField(_gravityAccelerationString, 6, GUILayout.Width(_valuesTextFieldsWidth));
		changeTextFieldAndSliderValue("gravityAcceleration", ref _gravityAcceleration, ref _gravityAccelerationString, newValue);
		GUILayout.EndHorizontal();

		// Simulation Speed
		GUILayout.BeginHorizontal();
		GUILayout.Box("Simulation Speed (fraction)", GUILayout.Width(_boxesWidth));
		newValue = GUILayout.HorizontalSlider(_simulationSpeed, 0.25f, 8.0f);
		_simulationSpeedString = GUILayout.TextField(_simulationSpeedString, 6, GUILayout.Width(_valuesTextFieldsWidth));
		changeTextFieldAndSliderValue("simulationSpeed", ref _simulationSpeed, ref _simulationSpeedString, newValue);
		GUILayout.EndHorizontal();

		// Height
		GUILayout.BeginHorizontal();
		GUILayout.Box("Height [m]", GUILayout.Width(_boxesWidth));
		newValue = GUILayout.HorizontalSlider(_height, 0.0f, 100.0f);
		_heightString = GUILayout.TextField(_heightString, 6, GUILayout.Width(_valuesTextFieldsWidth));
		changeTextFieldAndSliderValue("height", ref _height, ref _heightString, newValue);
		GUILayout.EndHorizontal();

		// Checkboxes
		GUILayout.BeginHorizontal();
		_isBallCameraEnabled = GUILayout.Toggle(_isBallCameraEnabled, "Camera on point");
		_isAirResistance = GUILayout.Toggle(_isAirResistance, "Air resistance (not implemented yet)");
		_isTrajectoryShowed = GUILayout.Toggle(_isTrajectoryShowed, "Show trajectory");
		if (_isBallCameraEnabled != cameraController.isBallCamEnabled) {
			cameraController.switchCamera();
		}
		projectileMotion.isTrajectoryShowed = _isTrajectoryShowed;
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		_isVectorVShowed = GUILayout.Toggle(_isVectorVShowed, "Show V");
		_isVectorVxShowed = GUILayout.Toggle(_isVectorVxShowed, "Show Vx");
		_isVectorVyShowed = GUILayout.Toggle(_isVectorVyShowed, "Show Vy");
		vectorVController.isShowed = _isVectorVShowed;
		vectorVxController.isShowed = _isVectorVxShowed;
		vectorVyController.isShowed = _isVectorVyShowed;
		GUILayout.EndHorizontal();

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	public void doAboutWindow(int id) {
		GUILayout.BeginArea(new Rect(5, 20, _aboutWindowWidth - 10, _aboutWindowHeight - 5));
		GUILayout.BeginVertical();
		GUILayout.Label("Projectile Motion Simulation\n\n" +
		                "Authors:\nProgrammer - Mateusz Przybył\nDesigner - Piotr Klemczak\n\n" +
		                "Runs on Unity3D engine (Free version), 3D models were made in Blender and are on CC BY-NC 3.0 license.\n\n" +
		                "Unity project and source code are on GPLv3 license and will be available on github.com/mrsimbax/projectilemotionunity\n\n" +
		                "This software is part of the educational project for school I Liceum Ogólnokształcące w Lesznie.");
		if (GUILayout.Button("OK")) {
			_isAboutWindowShowed = false;
		}

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	private void changeTextFieldAndSliderValue(string name, ref float originalValue, ref string stringValue, float newValue) {
		if (newValue != originalValue || stringValue != originalValue.ToString() && Event.current.keyCode == KeyCode.Return && float.TryParse(stringValue, out newValue)) {
			originalValue = changeValue(newValue, name);
			stringValue = Utilities.Round(originalValue);
		}
	}

	private float changeValue(float f, string name) {
		if (name == "initialVelocity") {
			projectileMotion.initialVelocity = f;
			return projectileMotion.initialVelocity;
		} else if (name == "launchAngle") {
			projectileMotion.launchAngle = f;
			return projectileMotion.launchAngle;
		} else if (name == "gravityAcceleration") {
			projectileMotion.gravityAcceleration = f;
			return projectileMotion.gravityAcceleration;
		} else if (name == "simulationSpeed") {
			projectileMotion.simulationSpeed = f;
			return projectileMotion.simulationSpeed;
		} else if (name == "height") {
			projectileMotion.height = f;
			return projectileMotion.height;
		} else {
			return -1.0f;
		}
	}
}
