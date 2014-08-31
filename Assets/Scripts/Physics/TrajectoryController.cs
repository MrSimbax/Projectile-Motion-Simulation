using UnityEngine;
using System.Collections;

public class TrajectoryController : MonoBehaviour {

    public TrailRenderer trailRenderer;

    public bool isShown {
        get { return _isShown; }
        set {
            if (value) { Show(); }
            else { Hide(); }
            _isShown = value;
        }
    }

    public void SwitchIsShown() {
        isShown = !_isShown;
    }

    public bool isRendering {
        get { return _isRendering; }
        set {
            if (value) { SetRenderingOn(); }
            else { SetRenderingOff(); }
            _isRendering = value;
        }
    }

    private bool _isShown;
    private bool _isRendering;

    private void SetRenderingOn() {
        trailRenderer.time = Mathf.Infinity;
    }

    private void SetRenderingOff() {
        trailRenderer.time = 0.0f;
    }

    private void Show() {
        trailRenderer.endWidth = 1.0f;
        trailRenderer.startWidth = 1.0f;
    }

    private void Hide() {
        trailRenderer.endWidth = 0.0f;
        trailRenderer.startWidth = 0.0f;
    }

}