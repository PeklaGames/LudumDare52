using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Transform _lookAtComponent;
    [SerializeField]
    private Animator _animator;
    private Rigidbody _rb;

    private int _angle = Animator.StringToHash("angle");
    private int _velocity = Animator.StringToHash("velocity");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var vel2d = new Vector2(_rb.velocity.x, _rb.velocity.z).normalized;
        var dir2d = new Vector2(_lookAtComponent.forward.x, _lookAtComponent.forward.z).normalized;
        Debug.Log("Velocity: " + _rb.velocity.magnitude + "; Dir: " + dir2d + "; Dot: " + Vector2.SignedAngle(vel2d, dir2d));
        //var dir = _rb.velocity.normalized - _lookAtComponent.forward;
        //;
        _animator.SetFloat(_angle, Vector2.SignedAngle(vel2d, dir2d));
        _animator.SetFloat(_velocity, _rb.velocity.magnitude);
    }
}
