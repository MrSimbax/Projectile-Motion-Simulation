using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayPauseButtonManager : MonoBehaviour {

    public SimulationController simulationController;
    public Button button;
    public Text buttonText;
    public delegate void BehaviourOnClick();
    public BehaviourOnClick behaviourOnClick;

    public void DoOnClick() {
        if (behaviourOnClick != null) {
            behaviourOnClick();
        }
    }

    public void OnEnable() {
        SimulationController.OnSimulationStart += ChangeBehaviourToPause;
        SimulationController.OnSimulationPause += ChangeBehaviourToPlay;
        SimulationController.OnSimulationReset += ChangeBehaviourToPlay;
        SimulationController.OnSimulationDone += ChangeBehaviourToDoNothing;
    }

    public void OnDisable() {
        SimulationController.OnSimulationStart -= ChangeBehaviourToPause;
        SimulationController.OnSimulationPause -= ChangeBehaviourToPlay;
        SimulationController.OnSimulationReset -= ChangeBehaviourToPlay;
        SimulationController.OnSimulationDone -= ChangeBehaviourToDoNothing;
    }

    public void Awake() {
        behaviourOnClick = simulationController.Play;
    }

    public void ChangeBehaviourToPause() {
        behaviourOnClick = simulationController.Pause;
        buttonText.text = "Pause";
    }

    public void ChangeBehaviourToPlay() {
        behaviourOnClick = simulationController.Play;
        button.interactable = true;
        buttonText.text = "Play";
    }

    public void ChangeBehaviourToDoNothing() {
        button.interactable = false;
        buttonText.text = "Done";
    }
}
