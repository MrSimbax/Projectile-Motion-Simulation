using UnityEngine;
using System.Collections;

public class ProjectileMotionData {
    public float xPos;
    public float yPos;
    public float zPos;
    public VelocityVector velocityVector;
    public float gravityAcceleration;
    public float time;
    public float deltaTime;

    public ProjectileMotionData() {
        xPos = 0.0f;
        yPos = 0.0f;
        zPos = 0.0f;
        velocityVector = new VelocityVector();
        gravityAcceleration = 9.81f;
        time = 0.0f;
        deltaTime = Time.fixedDeltaTime;
    }

    public ProjectileMotionData(ProjectileMotionData other) {
        xPos = other.xPos;
        yPos = other.yPos;
        zPos = other.zPos;
        velocityVector = new VelocityVector(other.velocityVector);
        gravityAcceleration = other.gravityAcceleration;
        time = other.time;
        deltaTime = other.deltaTime;
    }
}
