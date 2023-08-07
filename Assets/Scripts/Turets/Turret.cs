using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : UnitBehaviour
{
    public bool CanActivate = false;

    [Header("Weapon")]
    [SerializeField] private Transform _horizontalAimingAxis;
    [SerializeField] private Transform _verticalAimingAxis;
    [SerializeField] private float _range;
    [SerializeField] private float _aimingSpeed;
    [SerializeField] private float _aimingTolerance = 15f;
    [SerializeField] private float _shootTolerance = 15f;
    [SerializeField] private float _lookAround;
    [SerializeField] private BaseWeapon _weapon;
    [Header("Destroy")]
    [SerializeField] private float _destroyDelay;
    [SerializeField] private float _destroyUpSpeed;
    [SerializeField] private float _destroyRotationSpeed;
    [Header("Parenting")]
    [SerializeField] private float _equipRange;
    [SerializeField] private float _verticalOffset = 0.3f;
    [SerializeField] private float _parentingTime = 0.3f;
    [SerializeField] private Transform _explosionPrefab;
    [SerializeField] private Transform _dirtPrefab;

    private Transform _transform;
    protected private Player _player;
    protected private Transform _playerTransform;
    protected private AimingSystem _playerAiming;
    protected private Vector3 _aimingPoint;
    protected private State _state;

    protected private float _timer;
    private Vector3 _startPosition;
    private float _parentingTimer = -1;

    protected override void Awake()
    {
        base.Awake();
        _transform = transform;
        _player = MapGlobals.Instance.Player;
        _playerTransform = _player.transform;
        _playerAiming = MapGlobals.Instance.AimingSystem;
        _playerAiming.EquipPressed += TryEquip;
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
                if (Vector3.Angle(_verticalAimingAxis.forward, _playerTransform.position - transform.position) < _shootTolerance)
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

        if (_parentingTimer > 0)
            UpdateParenting();

    }

    private void TryEquip()
    {
        if (CheckEquip())
            if (_player.IsReadyForEquip)
                Equip();
    }

    private bool CheckEquip()
    {
        if (_transform == null)
            return false;

        return ((_transform.position - _playerTransform.position).sqrMagnitude < _equipRange * _equipRange);
    }

    private void UpdateParenting()
    {
        _parentingTimer -= Time.deltaTime / _parentingTime;

        _transform.position = Vector3.Lerp(_playerTransform.position + Vector3.up * _verticalOffset, _startPosition, Mathf.SmoothStep(0f, 1f, _parentingTimer));

        if (_parentingTimer <= 0)
        {
            _parentingTimer = -1;
            _transform.localPosition = Vector3.up * _verticalOffset;
        }
    }

    public void Equip()
    {
        _player.SetEquippedTurret(this);
        _weapon.OnPlayerEquip();
        _state = State.Equiped;
        _transform.parent = _playerTransform;
        _playerAiming.FirePressed += TryShoot;
        _playerAiming.UnequipPressed += Unequip;
        StartPositionChange();
    }

    private void StartPositionChange()
    {
        _startPosition = _transform.position;
        _parentingTimer = 1f;
    }

    public void Unequip()
    {
        DestroyTurret();
    }

    public override void Die()
    {
        DestroyTurret();
    }

    public void DestroyTurret()
    {
        if (_state == State.Equiped)
        {
            _player.SetEquippedTurret(null);
            _playerAiming.FirePressed -= TryShoot;
            _playerAiming.UnequipPressed -= Unequip;
            _transform.parent = null;
        }
        _state = State.Dead;
        var rig = _transform.GetComponent<Rigidbody>();
        rig.isKinematic = false;
        rig.velocity = Vector3.up * _destroyUpSpeed;
        rig.angularVelocity = _transform.forward * _destroyRotationSpeed;
        Destroy(gameObject, _destroyDelay);
    }

    private void OnDestroy()
    {
        Destroy(Instantiate(_explosionPrefab, _transform.position, _explosionPrefab.rotation).gameObject, 5f);
        Instantiate(_dirtPrefab, new Vector3(_transform.position.x, _dirtPrefab.position.y, _transform.position.z), _dirtPrefab.rotation);
    }

    private void TryFindPlayer()
    {
        if (CanActivate)
            if ((_playerTransform.position - _transform.position).sqrMagnitude < _range * _range)
            {
                _state = State.Battle;
            }
    }

    private void TryShoot()
    {
        if (_weapon.IsReady)
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
        if (_player.IsReadyForEquip)
        {
            _aimingPoint = _player.transform.position + Vector3.up * 0.2f;
        }
        else
        {
            _aimingPoint = _player.transform.position + Vector3.up;
        }
    }

    private void GetPointFromAiming()
    {
        _aimingPoint = _playerAiming.AimPoint + Vector3.up * 0.5f;
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


    private void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireSphere(_aimingPoint, 0.2f);
        UnityEditor.Handles.color = UnityEngine.Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _range);
        UnityEditor.Handles.color = UnityEngine.Color.cyan;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _equipRange);
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
