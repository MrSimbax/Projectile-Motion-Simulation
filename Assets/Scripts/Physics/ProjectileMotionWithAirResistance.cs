/*using UnityEngine;
using System.Collections;

public class ProjectileMotionWithAirResistance : ProjectileMotion {

    // Mass of the ball
    // kg
    // can't be changed during simulation
    public float mass {
        get { return _mass; }
        set {
            if (_isRunning) { return; }
            if (value < 0.0f) { return; }
            _mass = value;
        }
    }

    // Density of the air
    // kg / m^3
    // can't be changed during simulation
    public float densityOfAir {
        get { return _densityOfAir; }
        set {
            if (_isRunning) { return; }
            if (value < 0.0f) { return; }
            _densityOfAir = value;
        }
    }

    // Cross sectional area of the ball
    // m^2
    // can't be changed
    public float crossSectionalArea {
        get { return _crossSectionalArea; }
    }

    // Drag coefficient
    // -
    // can't be changed
    public float dragCoefficient {
        get { return _dragCoefficient; }
    }

    private float _mass;
    private float _densityOfAir;
    private float _crossSectionalArea;
    private float _dragCoefficient;

    private float _airDragForce;

    private void calculateAirDragForce(float velocity) {
        _airDragForce = 0.5f * _densityOfAir * _crossSectionalArea * _dragCoefficient *
                        Mathf.Pow(velocity, 2.0f);
        if (velocity < 0.0f) _airDragForce *= -1.0f;
    }

    protected override void calculateCurrentVelocity() {
        calculateAirDragForce(_velocityVector.horizontal);
        float horizontal = - (_airDragForce / _mass) * _deltaTime;

        calculateAirDragForce(_velocityVector.vertical);
        float vertical = - (_gravityAcceleration + (_airDragForce / _mass)) * _deltaTime;

        _velocityVector.UpdateVector(horizontal, vertical);
        calculateAirDragForce(_velocityVector.magnitude);
    }

    public override void setDefaultSettings() {
        _mass = 1.0f;
        _densityOfAir = 1.2f;

        // For the sphere with the radius of 0.05 meters
        _crossSectionalArea = Mathf.PI * Mathf.Pow(0.05f, 2.0f);
        _dragCoefficient = 0.5f;

        base.setDefaultSettings();
    }
}*/
