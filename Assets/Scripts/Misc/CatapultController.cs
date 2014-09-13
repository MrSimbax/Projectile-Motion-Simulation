using UnityEngine;
using System.Collections;

public class CatapultController : MonoBehaviour {
    public CatapultAnimationController catAnimController;
    public Transform pmTransform;
    public Transform pmEntityTransform;

    public void Start() {
        catAnimController.stretch();
    }

    private void DoNotMoveCatapultWithEntity() {
        transform.parent = pmTransform;
    }

    private void MoveCatapultWithEntity() {
        transform.parent = pmEntityTransform;
    }

    public void JustThrow() {
        DoNotMoveCatapultWithEntity();
        catAnimController.throw_();
    }

    public void JustReset() {
        MoveCatapultWithEntity();
        catAnimController.reset();
    }
}