using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    public SimulationController simulationController;

    public InputField velocityInput;
    public InputField angleInput;
    public InputField heightInput;
    public InputField gravityAccInput;

    public void Start() {
        velocityInput.onSubmit.AddListener(ChangeVelocity);
        angleInput.onSubmit.AddListener(ChangeAngle);
        heightInput.onSubmit.AddListener(ChangeHeight);
        gravityAccInput.onSubmit.AddListener(ChangeGravityAcc);

        UpdateInputs();
    }

    public void OnEnable() {
        //ProjectileMotionData.SomethingHasChanged += UpdateIns;
        SimulationController.OnSimulationStart += DisableInputs;
        SimulationController.OnSimulationReset += EnableInputs;
        SimulationController.OnSimulationReset += UpdateInputs;
    }

    public void OnDisable() {
        //ProjectileMotionData.SomethingHasChanged -= UpdateIns;
        SimulationController.OnSimulationStart -= DisableInputs;
        SimulationController.OnSimulationReset -= EnableInputs;
        SimulationController.OnSimulationReset -= UpdateInputs;
    }

    public void UpdateInputs() {
        ProjectileMotionData data = simulationController.initData;
        velocityInput.value = Utilities.Round(data.velocityVector.magnitude);
        angleInput.value = Utilities.Round(data.velocityVector.angle);
        heightInput.value = Utilities.Round(data.yPos);
        gravityAccInput.value = Utilities.Round(data.gravityAcceleration);
    }

    public void EnableInputs() {
        velocityInput.interactable = true;
        angleInput.interactable = true;
        heightInput.interactable = true;
        gravityAccInput.interactable = true;
    }

    public void DisableInputs() {
        velocityInput.interactable = false;
        angleInput.interactable = false;
        heightInput.interactable = false;
        gravityAccInput.interactable = false;
    }
    /*
    public void UpdateIns() {
        ProjectileMotionData data = simulationController.currentData;
        velocityInput.value = Utilities.Round(data.velocityVector.magnitude);
        angleInput.value = Utilities.Round(data.velocityVector.angle);
        heightInput.value = Utilities.Round(data.yPos);
        gravityAccInput.value = Utilities.Round(data.gravityAcceleration);
    }
*/

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
}
