using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour {

    public InputField timeInput;
    public InputField deltaTimeInput;
    public InputField xPosInput;
    public InputField yPosInput;
    public InputField angleInput;
    public InputField velocityInput;
    public InputField velocityXInput;
    public InputField velocityYInput;
    public InputField maxYInput;
    public InputField maxYTimeInput;

    public SimulationController simulationController;

    private ProjectileMotionData _data;

    public void OnEnable() {
        SimulationController.OnSimulationReset += UpdateStats;
        ProjectileMotion.OnNextStep += UpdateStats;
    }

    public void OnDisable() {
        SimulationController.OnSimulationReset -= UpdateStats;
        ProjectileMotion.OnNextStep -= UpdateStats;
    }

    public void UpdateStats() {
        _data = simulationController.currentData;
        if (_data == null) {
            Debug.LogError("StatisticsManager: _data is null");
            return;
        }
        timeInput.value = Utilities.Round(_data.time);
        deltaTimeInput.value = Utilities.Round(_data.deltaTime);
        xPosInput.value = Utilities.Round(_data.xPos);
        yPosInput.value = Utilities.Round(_data.yPos);
        angleInput.value = Utilities.Round(_data.velocityVector.angle);
        velocityInput.value = Utilities.Round(_data.velocityVector.magnitude);
        velocityXInput.value = Utilities.Round(_data.velocityVector.horizontal);
        velocityYInput.value = Utilities.Round(_data.velocityVector.vertical);
        maxYInput.value = Utilities.Round(_data.maxY);
        maxYTimeInput.value = Utilities.Round(_data.maxYTime);
    }
}
