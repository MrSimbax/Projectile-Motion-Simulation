/*using UnityEngine;
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
            "Time: "+Utilities.Round(projectileMotion.time)+" seconds\n"+
            "Delta Time: "+Utilities.Round(projectileMotion.deltaTime)+" seconds\n"+
            "Horizontal Position: "+Utilities.Round(projectileMotion.ball.
                                                     transform.localPosition.x)+" meters\n"+
            "Vertical Position: "+Utilities.Round(projectileMotion.ball.
                                                     transform.localPosition.y)+" meters\n"+
            "Velocity: "+Utilities.Round(projectileMotion.velocityVector.magnitude)+
                                                                        " meters / seconds\n"+
            "Horizontal Velocity: "+Utilities.Round(projectileMotion.velocityVector.horizontal)+
                                                                             " meters / seconds\n"+
            "Vertical Velocity: "+Utilities.Round(projectileMotion.velocityVector.vertical)+
                                                                            " meteres / seconds\n"+
            "Angle: " + Utilities.Round(projectileMotion.velocityVector.angle) + " degrees\n"+
            "Max Height: "+Utilities.Round(projectileMotion.maxHeight) + " meteres\n"+
            "Time When Reached Max Height: "+Utilities.Round(projectileMotion.
                                                           timeWhenReachedMaxHeight)+" seconds\n";
    }
}
*/