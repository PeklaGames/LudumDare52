using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 _moveDir;

    [SerializeField]
    private float _speedMax;
    [SerializeField]
    private float _acceleration;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        _rb.AddRelativeForce(new Vector3(_moveDir.x, 0, _moveDir.y) * _speedMax);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _speedMax);
    }


    public void OnMove(InputValue value) => _moveDir = value.Get<Vector2>().normalized;
}
