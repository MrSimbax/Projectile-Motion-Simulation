using UnityEngine;
using System.Collections;

public class ShowStats : MonoBehaviour {

    //
    // Gui text behaviour
    //

    // Must be provided in the inspector:
    // ----------------------------------
    public ProjectileMotion projectileMotion;
    public GameObject ball;


    // Behaviour
    // ---------
    void Start () {
        // Scale the size of the text
        guiText.fontSize = Screen.height / 32;
    }

    void Update () {
        guiText.text =
            "Current Time: " + Utilities.Round(projectileMotion.time) + " seconds\n" +
            "Delta Time: " + Utilities.Round(projectileMotion.deltaTime) + " seconds\n" +
            "Current X Position: " + Utilities.Round(projectileMotion.ball.transform.localPosition.x) + " meters\n" +
            "Current Y Position: " + Utilities.Round(projectileMotion.ball.transform.localPosition.y) + " meters\n" +
            "Current Velocity: " +  Utilities.Round(projectileMotion.velocity) + " meters / seconds\n" +
            "Current Horizontal Velocity: " + Utilities.Round(projectileMotion.horizontalVelocity) + " meters / seconds\n" +
            "Current Vertical Velocity: " + Utilities.Round(projectileMotion.verticalVelocity) + " meteres / seconds\n" +
            "Current Angle: " + Utilities.Round(projectileMotion.angle) + " degrees\n" +
            "Max Height: " + Utilities.Round(projectileMotion.maxHeight) + " meteres\n" +
            "Time When Reached Max Height: " + Utilities.Round(projectileMotion.timeWhenReachedMaxHeight) + " seconds\n";
    }
}
