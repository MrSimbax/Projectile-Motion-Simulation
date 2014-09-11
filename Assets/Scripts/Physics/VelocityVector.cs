using UnityEngine;
using System.Collections;

public class VelocityVector {

    public delegate void VelocityVectorAction();
    public event VelocityVectorAction SomethingHasChanged;

    public float magnitude {
        get { return _magnitude; }
    }

    public float horizontal {
        get { return _horizontal; }
    }

    public float vertical {
        get { return _vertical; }
    }

    // Angle is in degrees
    public float angle {
        get { return _angle; }
    }

    public float radAngle {
        get { return _angle * Mathf.Deg2Rad; }
    }

    private float _magnitude;
    private float _horizontal;
    private float _vertical;
    private float _angle;

    public VelocityVector() {
        SetVector(0.0f, 0.0f);
    }

    public VelocityVector(VelocityVector other) {
        SetVector(other.magnitude, other.angle);
    }

    public void UpdateVector(float aDeltaHorizontal, float aDeltaVertical) {
        _vertical += aDeltaVertical;
        _horizontal += aDeltaHorizontal;

        _magnitude = Mathf.Sqrt(
                Mathf.Pow(_vertical, 2.0f) + Mathf.Pow(_horizontal, 2.0f)
            );
        _angle = CalculateAngle();
        if (SomethingHasChanged != null) {
            SomethingHasChanged();
        }
    }

    public void SetVector(float aMagnitude, float aAngle) {
        if (aMagnitude < 0.0f) {
            Debug.LogError ("VelocityVector: Magnitude can't be less than zero.");
            return;
        }
        _magnitude = aMagnitude;
        _vertical = aMagnitude * Mathf.Sin(aAngle * Mathf.Deg2Rad);
        _horizontal = aMagnitude * Mathf.Cos(aAngle * Mathf.Deg2Rad);
        _angle = aAngle;
        if (SomethingHasChanged != null) {
            SomethingHasChanged();
        }
    }

    private float CalculateAngle() {
        if (!Utilities.isZero(_horizontal)) {
            return Mathf.Atan(_vertical / _horizontal) * Mathf.Rad2Deg;
        } else {
            if (_vertical < 0.0f) { return -90.0f; }
            else { return 90.0f; }
        }
    }
}