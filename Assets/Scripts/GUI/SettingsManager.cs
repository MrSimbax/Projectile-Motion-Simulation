using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    public SimulationController simulationController;

    public InputField velocityInput;
    public InputField angleInput;
    public InputField heightInput;
    public InputField gravityAccInput;

    public InputField massInput;
    public InputField airDensityInput;
    public InputField dragCoefficientInput;
    public InputField areaInput;

    public Toggle airDragToggle;

    public void Start() {
        velocityInput.onSubmit.AddListener(ChangeVelocity);
        angleInput.onSubmit.AddListener(ChangeAngle);
        heightInput.onSubmit.AddListener(ChangeHeight);
        gravityAccInput.onSubmit.AddListener(ChangeGravityAcc);
        massInput.onSubmit.AddListener(ChangeMass);
        airDensityInput.onSubmit.AddListener(ChangeAirDensity);
        dragCoefficientInput.onSubmit.AddListener(ChangeDragCoefficient);
        areaInput.onSubmit.AddListener(ChangeArea);
        UpdateInputs();
    }

    public void OnEnable() {
        //ProjectileMotionData.SomethingHasChanged += UpdateIns;
        SimulationController.OnSimulationStart += DisableAllInputs;
        SimulationController.OnSimulationReset += EnableAllInputs;
        SimulationController.OnSimulationReset += UpdateInputs;
        airDragToggle.onValueChanged.AddListener(simulationController.SwitchIsAirDrag);
        airDragToggle.onValueChanged.AddListener(SwitchAirDrag);
    }

    public void OnDisable() {
        //ProjectileMotionData.SomethingHasChanged -= UpdateIns;
        SimulationController.OnSimulationStart -= DisableAllInputs;
        SimulationController.OnSimulationReset -= EnableAllInputs;
        SimulationController.OnSimulationReset -= UpdateInputs;
        airDragToggle.onValueChanged.RemoveListener(simulationController.SwitchIsAirDrag);
        airDragToggle.onValueChanged.RemoveListener(SwitchAirDrag);
    }

    public void SetAirDragInputsInteractivity() {
        if (airDragToggle.isOn && airDragToggle.IsInteractable()) {
            EnableAirDragInputs();
        } else {
            DisableAirDragInputs();
        }
    }

    public void SwitchAirDrag(bool value) {
        if (value) {
            EnableAirDragInputs();
        } else {
            DisableAirDragInputs();
        }
    }

    public void UpdateInputs() {
        ProjectileMotionData data = simulationController.initData;
        velocityInput.value = Utilities.Round(data.velocityVector.magnitude);
        angleInput.value = Utilities.Round(data.velocityVector.angle);
        heightInput.value = Utilities.Round(data.yPos);
        gravityAccInput.value = Utilities.Round(data.gravityAcceleration);

        airDragToggle.onValueChanged.RemoveListener(simulationController.SwitchIsAirDrag);
        airDragToggle.onValueChanged.RemoveListener(SwitchAirDrag);
        airDragToggle.isOn = data.isAirDrag;
        airDragToggle.onValueChanged.AddListener(simulationController.SwitchIsAirDrag);
        airDragToggle.onValueChanged.AddListener(SwitchAirDrag);
        //Debug.Log (airDragToggle.isOn);

        massInput.value = Utilities.Round(data.mass);
        airDensityInput.value = Utilities.Round(data.airDensity);
        dragCoefficientInput.value = Utilities.Round(data.dragCoefficient);
        areaInput.value = Utilities.Round(data.crossSectionalArea);
    }

    public void EnableAllInputs() {
        velocityInput.interactable = true;
        angleInput.interactable = true;
        heightInput.interactable = true;
        gravityAccInput.interactable = true;
        airDragToggle.interactable = true;
        SetAirDragInputsInteractivity();
    }

    public void DisableAllInputs() {
        velocityInput.interactable = false;
        angleInput.interactable = false;
        heightInput.interactable = false;
        gravityAccInput.interactable = false;
        airDragToggle.interactable = false;
        SetAirDragInputsInteractivity();
    }

    public void EnableAirDragInputs() {
        massInput.interactable = true;
        airDensityInput.interactable = true;
        dragCoefficientInput.interactable = true;
        areaInput.interactable = true;
    }

    public void DisableAirDragInputs() {
        massInput.interactable = false;
        airDensityInput.interactable = false;
        dragCoefficientInput.interactable = false;
        areaInput.interactable = false;
    }

    public float ParseValue(string value, InputField inputField, float min, float max) {
        float f;

        if (!float.TryParse(value, out f)) {
            Debug.LogError("SettingsManager: could not parse the value!");
            return Mathf.Infinity;
        }
        
        if (f < min) {
            f = min;
        } else if (f > max) {
            f = max;
        }

        inputField.value = Utilities.Round(f);

        return f;
    }

    public void ChangeVelocity(string value) {
        float velocity = ParseValue(value, velocityInput, 0.0f, 100.0f);
        if (velocity == Mathf.Infinity) { return; }
        simulationController.initData.velocityVector.SetVector(velocity, simulationController.currentData.velocityVector.angle);
    }

    public void ChangeAngle(string value) {
        float angle = ParseValue(value, angleInput, 0.0f, 90.0f);
        if (angle == Mathf.Infinity) { return; }
        simulationController.initData.velocityVector.SetVector(simulationController.currentData.velocityVector.magnitude, angle);
    }

    public void ChangeHeight(string value) {
        float height = ParseValue(value, heightInput, 0.0f, 100.0f);
        if (height == Mathf.Infinity) { return; }
        simulationController.initData.yPos = height;
    }

    public void ChangeGravityAcc(string value) {
        float gravityAcc = ParseValue(value, gravityAccInput, 0.0f, 100.0f);
        if (gravityAcc == Mathf.Infinity) { return; }
        simulationController.initData.gravityAcceleration = gravityAcc;
    }

    public void ChangeMass(string value) {
        float mass = ParseValue(value, massInput, 0.0f, 100.0f);
        if (mass == Mathf.Infinity) { return; }
        simulationController.initData.mass = mass;
    }

    public void ChangeAirDensity(string value) {
        float airDensity = ParseValue(value, airDensityInput, 0.0f, 999.0f);
        if (airDensity == Mathf.Infinity) { return; }
        simulationController.initData.airDensity = airDensity;
    }

    public void ChangeDragCoefficient(string value) {
        float dragCoefficient = ParseValue(value, dragCoefficientInput, 0.0f, 10.0f);
        if (dragCoefficient == Mathf.Infinity) { return; }
        simulationController.initData.dragCoefficient = dragCoefficient;
    }

    public void ChangeArea(string value) {
        float area = ParseValue(value, areaInput, 0.0f, 1.0f);
        if (area == Mathf.Infinity) { return; }
        simulationController.initData.crossSectionalArea = area;
    }
}
