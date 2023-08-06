using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _horizontalAimingAxis;
    [SerializeField] private Transform _verticalAimingAxis;
    [SerializeField] private float _aimingSpeed;
    [SerializeField] private float _lookAround;
    [SerializeField] private float _delayBeforeNextAttack;

    protected private Player _player;
    protected private AimingSystem _playerAiming;
    protected private Vector3 _aimingPoint;
    protected private State _state;

    protected private float _timer;



    private void Awake()
    {
        _player = MapGlobals.Instance.Player;
        _playerAiming = MapGlobals.Instance.AimingSystem;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Idle:
                LookAround();
                Aiming();
                break;
            case State.Battle:
                GetDataFromPlayerPosition();
                Aiming();
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

    private void LookAround()
    {
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
        throw new NotImplementedException();
    }

    public virtual void Shoot()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_aimingPoint, 0.2f);
    }

    public enum State
    {
        Idle,
        Battle,
        Equiped,
        Dead
    }
}
