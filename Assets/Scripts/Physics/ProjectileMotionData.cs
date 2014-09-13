using UnityEngine;
using System.Collections;

public class ProjectileMotionData {

    public delegate void ProjectileMotionDataAction();
    public event ProjectileMotionDataAction SomethingHasChanged;

    public bool isAirDrag {
        get { return _isAirDrag; }
        set {
            _isAirDrag = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }

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

    public float mass {
        get { return _mass; }
        set {
            _mass = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }

    public float airDensity {
        get { return _airDensity; }
        set {
            _airDensity = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }

    public float dragCoefficient {
        get { return _dragCoefficient; }
        set {
            _dragCoefficient = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }

    public float crossSectionalArea {
        get { return _crossSectionalArea; }
        set {
            _crossSectionalArea = value;
            if (SomethingHasChanged != null) {
                SomethingHasChanged();
            }
        }
    }

    private bool _isAirDrag;
    private float _xPos;
    private float _yPos;
    private float _zPos;
    private VelocityVector _velocityVector;
    private float _gravityAcceleration;
    private float _time;
    private float _deltaTime;
    private float _maxY;
    private float _maxYTime;
    private float _mass;
    private float _airDensity;
    private float _dragCoefficient;
    private float _crossSectionalArea;

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
        isAirDrag = false;
        mass = 1.0f;
        airDensity = 1.225f;
        dragCoefficient = 0.5f;
        crossSectionalArea = 0.0314f;
        
        velocityVector.SomethingHasChanged += NotifyAboutVelocityVectorChange;
    }

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
        isAirDrag = other.isAirDrag;
        mass = other.mass;
        airDensity = other.airDensity;
        dragCoefficient = other.dragCoefficient;
        crossSectionalArea = other.crossSectionalArea;
    }

    ~ProjectileMotionData() {
        velocityVector.SomethingHasChanged -= NotifyAboutVelocityVectorChange;
    }

    private void NotifyAboutVelocityVectorChange() {
        if (SomethingHasChanged != null) {
            SomethingHasChanged();
        }
    }
}
