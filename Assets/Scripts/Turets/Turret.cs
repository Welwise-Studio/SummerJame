using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Turret : UnitBehaviour
{
    [SerializeField] private Transform _horizontalAimingAxis;
    [SerializeField] private Transform _verticalAimingAxis;
    [SerializeField] private float _range;
    [SerializeField] private float _aimingSpeed;
    [SerializeField] private float _aimingTolerance = 15f;
    [SerializeField] private float _shootTolerance = 15f;
    [SerializeField] private float _lookAround;
    [SerializeField] private float _delayBeforeNextAttack;
    [SerializeField] private BaseWeapon _weapon;

    private Transform _transform;
    protected private Player _player;
    protected private Transform _playerTransform;
    protected private AimingSystem _playerAiming;
    protected private Vector3 _aimingPoint;
    protected private State _state;

    protected private float _timer;

    protected override void Awake()
    {
        base.Awake();
        _transform = transform;
        _player = MapGlobals.Instance.Player;
        _playerTransform = _player.transform;
        _playerAiming = MapGlobals.Instance.AimingSystem;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Idle:
                LookAround();
                Aiming();
                TryFindPlayer();
                break;
            case State.Battle:
                GetDataFromPlayerPosition();
                Aiming();
                TryShoot();
                break;
            case State.Equiped:
                GetPointFromAiming();
                Aiming();
                break;
            case State.Dead:
                break;
            default:
                break;
        }


    }

    private void TryFindPlayer()
    {
        if ((_playerTransform.position - _transform.position).sqrMagnitude < _range * _range)
        {
            _state = State.Battle;
        }
    }

    private void TryShoot()
    {
        _weapon.Shoot();
    }

    private void LookAround()
    {
        Vector3 point = Vector3.forward * 10f;
        Quaternion quaternion = Quaternion.Euler(0, Time.time * _lookAround, 0);
        _aimingPoint = quaternion * point + _transform.position;
    }

    private void GetDataFromPlayerPosition()
    {
        _aimingPoint = _player.transform.position;
    }

    private void GetPointFromAiming()
    {
        _aimingPoint = _playerAiming.AimPoint;
    }

    private void Aiming()
    {
        var forward = _horizontalAimingAxis.forward;
        var right = _horizontalAimingAxis.right;
        var toPoint = (_aimingPoint - _verticalAimingAxis.position).normalized;

        var angleHorizontal = Mathf.Clamp((Vector3.Angle(forward, -toPoint) * Mathf.Sign(-Vector3.Dot(toPoint, right))) / _aimingTolerance, -1, 1);
        _horizontalAimingAxis.Rotate(angleHorizontal < 0 ? Vector3.up : Vector3.down, Time.deltaTime * _aimingSpeed * Mathf.Abs(angleHorizontal), Space.Self);

        forward = _verticalAimingAxis.forward;
        right = _verticalAimingAxis.right;

        var angleVertical = Mathf.Clamp((Vector3.SignedAngle(forward, toPoint, right)) / _aimingTolerance, -1, 1);
        _verticalAimingAxis.Rotate(Vector3.right, Time.deltaTime * _aimingSpeed * angleVertical, Space.Self);
    }

    public virtual void Shoot()
    {

    }

    private void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireSphere(_aimingPoint, 0.2f);
        UnityEditor.Handles.color = UnityEngine.Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _range);
#endif

    }

    public enum State
    {
        Idle,
        Battle,
        Equiped,
        Dead
    }
}
