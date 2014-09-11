using UnityEngine;
using System.Collections;

public class ProjectileMotionData {

    public delegate void ProjectileMotionDataAction();
    public event ProjectileMotionDataAction SomethingHasChanged;

    public float xPos {
        get { return _xPos; }
        set {
            _xPos = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }

    public float yPos {
        get { return _yPos; }
        set {
            _yPos = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }
    public float zPos {
        get { return _zPos; }
        set {
            _zPos = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }
    public VelocityVector velocityVector {
        get { return _velocityVector; }
        set {
            _velocityVector = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }
    public float gravityAcceleration {
        get { return _gravityAcceleration; }
        set {
            _gravityAcceleration = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }
    public float time {
        get { return _time; }
        set {
            _time = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }
    public float deltaTime {
        get { return _deltaTime; }
        set {
            _deltaTime = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }
    public float maxY {
        get { return _maxY; }
        set {
            _maxY = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }

    public float maxYTime {
        get { return _maxYTime; }
        set {
            _maxYTime = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }

    public ProjectileMotionData() {
        xPos = 0.0f;
        yPos = 0.0f;
        zPos = 0.0f;
        velocityVector = new VelocityVector();
        gravityAcceleration = 9.81f;
        time = 0.0f;
        deltaTime = Time.fixedDeltaTime;
        maxY = 0.0f;
        maxYTime = 0.0f;

        velocityVector.SomethingHasChanged += NotifyAboutVelocityVectorChange;
    }

    ~ProjectileMotionData() {
        velocityVector.SomethingHasChanged -= NotifyAboutVelocityVectorChange;
    }

    private float _xPos;
    private float _yPos;
    private float _zPos;
    private VelocityVector _velocityVector;
    private float _gravityAcceleration;
    private float _time;
    private float _deltaTime;
    private float _maxY;
    private float _maxYTime;


    public ProjectileMotionData(ProjectileMotionData other) {
        xPos = other.xPos;
        yPos = other.yPos;
        zPos = other.zPos;
        velocityVector = new VelocityVector(other.velocityVector);
        gravityAcceleration = other.gravityAcceleration;
        time = other.time;
        deltaTime = other.deltaTime;
        maxY = other.maxY;
        maxYTime = other.maxYTime;
    }

    private void NotifyAboutVelocityVectorChange() {
        if (SomethingHasChanged != null) {
            SomethingHasChanged();
        }
    }
}
