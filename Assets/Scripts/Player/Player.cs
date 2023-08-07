using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : UnitBehaviour
{
    [SerializeField] private float _rotationForce;
    [SerializeField] private float _rotationError;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _equippedForce;
    [SerializeField] private AimingSystem _aimingSystem;
    private Turret _turret;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private bool _controllable;

    public bool IsReadyForEquip => _turret == null;

    internal void SetEquippedTurret(Turret turret)
    {
        _turret = turret;
    }

    public override void Die()
    {
        //base.Die();
        MapGlobals.Instance.OnPlayerDead();
        DisableControls();
    }

    private void DisableControls()
    {
        _controllable = false;
    }

    protected override void Awake()
    {
        _controllable = true;
        base.Awake();
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_controllable == false)
            return;

        var inputForce = (Vector3.forward + Vector3.right) * Input.GetAxis("Vertical") + (Vector3.right - Vector3.forward) * Input.GetAxis("Horizontal");

        if (inputForce.sqrMagnitude > 1)
        {
            inputForce.Normalize();
        }

        inputForce *= IsReadyForEquip ? _maxForce : _equippedForce;

        _rigidbody.AddForce(inputForce, ForceMode.Force);

        var angle = Vector3.Angle(_transform.forward, _aimingSystem.AimPoint - _transform.position);
        angle *= Mathf.Sign(Vector3.Dot(_aimingSystem.AimPoint - _transform.position, _transform.right));
        angle = Mathf.Clamp(angle, -_rotationError, _rotationError);
        angle /= _rotationError;
        _rigidbody.AddTorque(Vector3.up * _rotationForce * angle);
    }
}
