using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour {

    public InputField timeInput;
    public InputField deltaTimeInput;
    public InputField xPosInput;
    public InputField yPosInput;
    public InputField velocityInput;
    public InputField velocityXInput;
    public InputField velocityYInput;
    public InputField maxYInput;
    public InputField maxYTimeInput;

    public SimulationController simulationController;

    private ProjectileMotionData _data;

    public void OnEnable() {
        SimulationController.OnSimulationStart += GetDataFromSimulation;
        SimulationController.OnSimulationReset += GetDataFromController;
        SimulationController.OnSimulationReset += UpdateStats;
        ProjectileMotion.OnNextStep += UpdateStats;
    }

    public void OnDisable() {
        SimulationController.OnSimulationStart -= GetDataFromSimulation;
        ProjectileMotion.OnNextStep -= UpdateStats;
    }

    public void UpdateStats() {
        if (_data == null) {
            Debug.LogError("StatisticsManager: _data is null");
            return;
        }
        timeInput.value = Utilities.Round(_data.time);
        deltaTimeInput.value = Utilities.Round(_data.deltaTime);
        xPosInput.value = Utilities.Round(_data.xPos);
        yPosInput.value = Utilities.Round(_data.yPos);
        velocityInput.value = Utilities.Round(_data.velocityVector.magnitude);
        velocityXInput.value = Utilities.Round(_data.velocityVector.horizontal);
        velocityYInput.value = Utilities.Round(_data.velocityVector.vertical);
    }

    public void GetDataFromSimulation() {
        _data = simulationController.projectileMotion.data;
    }

    public void GetDataFromController() {
        _data = simulationController.initData;
    }
}
