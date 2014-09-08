using UnityEngine;
using System.Collections;

public class ProjectileMotion {

    public ProjectileMotionData data;

    public delegate void ProjectileMotionAction();
    public static event ProjectileMotionAction OnNextStep;

    public ProjectileMotion() {
        data = new ProjectileMotionData();
        data.yPos = 0.0f;
        data.velocityVector.SetVector(10.0f, 45.0f);
        data.gravityAcceleration = 9.81f;
    }

    public ProjectileMotion(ProjectileMotionData aPmData) {
        data = new ProjectileMotionData(aPmData);
    }

    private void CalculateVelocity() {
        float deltaHorizontalVelocity = 0.0f;
        float deltaVerticalVelocity = - data.gravityAcceleration * data.deltaTime;
        data.velocityVector.UpdateVector(deltaHorizontalVelocity, deltaVerticalVelocity);
    }

    private void CalculateNextPosition() {
        data.xPos += data.velocityVector.horizontal * data.deltaTime;
        data.yPos += data.velocityVector.vertical * data.deltaTime;
        data.zPos += 0.0f;
    }

    private void UpdateTime() {
        data.time += data.deltaTime;
    }

    public void StepToNextPosition() {
        CalculateVelocity();
        CalculateNextPosition();
        UpdateTime();
        if (OnNextStep != null) {
            OnNextStep();
        }
    }
}
