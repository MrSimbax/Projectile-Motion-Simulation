using UnityEngine;
using System.Collections;

public class AboutPanel : MonoBehaviour {

    bool _isActive;

    public void Awake() {
        _isActive = gameObject.activeSelf;
    }

    public void SwitchEnabled() {
        _isActive = !_isActive;
        gameObject.SetActive(_isActive);
    }
}
