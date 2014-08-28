using UnityEngine;
using System.Collections;

public class BallBaseController : MonoBehaviour {
    
    public Transform ballTransform;

    public float height {
        get { return _height; }
        set {
            if (value < 0.0f) { 
                Debug.LogError("BallBaseController: Height can't be less than zero.");
                return;
            }

            float ballRadius = ballTransform.localScale.y / 2.0f;

            if (value <= ballRadius / 2.0f) {
                renderer.enabled = false;
            } else {
                renderer.enabled = true;
            }

            float baseHeight = value - ballRadius;
            transform.localPosition = new Vector3(0.0f, baseHeight / 2.0f, 0.0f);
            transform.localScale = new Vector3(1.0f, baseHeight / 2.0f, 1.0f);

            _height = value;
        }
    }

    private float _height;
}