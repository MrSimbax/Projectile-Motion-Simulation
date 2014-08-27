using UnityEngine;
using System.Collections;

public class CatapultAnimationController : MonoBehaviour {
    
    private Animator _animator;

    private int _stretchHash = Animator.StringToHash("Stretch");
    private int _throwHash = Animator.StringToHash("Throw");
    private int _idle1StateHash = Animator.StringToHash("Base Layer.Idle");
    private int _idle2StateHash = Animator.StringToHash("Base Layer.Idle 2");

    private bool _doStretch;
    private bool _doThrow;
    private bool _doReset;

    
    // Public methods
    // --------------

    public void stretch() {
        _doStretch = true;
    }

    public void throw_() {
        _doThrow = true;
    }

    public void reset() {
        _doReset = true;
    }

    
    // Behaviour
    // ---------

    void Start () {
        _animator = GetComponent<Animator>();
        _doStretch = false;
        _doThrow = false;
        _doReset = false;
    }

    void Update () {
        AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (_doStretch && animatorStateInfo.nameHash == _idle1StateHash) {
            _animator.SetTrigger(_stretchHash);
            _doStretch = false;
        }

        if (_doThrow && animatorStateInfo.nameHash == _idle1StateHash) {
            _animator.SetTrigger(_throwHash);
            _doThrow = false;
        }

        if (_doReset && animatorStateInfo.nameHash == _idle2StateHash) {
            _animator.SetTrigger(_stretchHash);
            _doReset = false;
        }
    }
}
