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
        data.mass = 1.0f;
        data.airDensity = 1.225f;
        data.dragCoefficient = 0.5f;
        data.crossSectionalArea = 0.0314f;
        data.isAirDrag = false;
    }

    public ProjectileMotion(ProjectileMotionData aPmData) {
        data = new ProjectileMotionData(aPmData);
    }

    private float CalculateAirDragForce(float velocity) {
        return 0.5f * data.airDensity * data.crossSectionalArea * data.dragCoefficient *
            Mathf.Pow(velocity, 2.0f) * (velocity >= 0.0f ? 1.0f : -1.0f);
    }

    private void CalculateVelocity() {
        float deltaHorizontalVelocity;
        float deltaVerticalVelocity;
        if (data.isAirDrag) {
            float airDragForce = 0.0f;
            airDragForce = CalculateAirDragForce(data.velocityVector.horizontal);
            //Debug.Log (data.mass);
            //Debug.Log (airDragForce);
            deltaHorizontalVelocity = - (airDragForce / data.mass) * data.deltaTime;
            airDragForce = CalculateAirDragForce(data.velocityVector.vertical);
            deltaVerticalVelocity = - (data.gravityAcceleration + (airDragForce / data.mass)) * data.deltaTime;
        } else {
            deltaHorizontalVelocity = 0.0f;
            deltaVerticalVelocity = - data.gravityAcceleration * data.deltaTime;
        }
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

    private void UpdateMaxHeight() {
        if (data.yPos > data.maxY) {
            data.maxY = data.yPos;
            data.maxYTime = data.time;
        }
    }

    public void StepToNextPosition() {
        CalculateVelocity();
        CalculateNextPosition();
        UpdateTime();
        UpdateMaxHeight();
        if (OnNextStep != null) {
            OnNextStep();
        }
    }
}
