using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.PackageManager;
using UnityEngine;

public class WalkerLeg : MonoBehaviour
{

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _idealPoint;
    [SerializeField] private float _stepZone;
    [SerializeField] private float _maxZone;
    [SerializeField] private float _errorZone;
    [SerializeField] private float _stepTime = 0.3f;
    [SerializeField] private float _stepHeight = 0.1f;
    [SerializeField] private float _stepAngle = 15;

    [SerializeField] private bool _debug;

    private Transform _transform;
    private Vector3 _stepPoint;
    private float _timer;
    private Vector3 _startPosition;
    private float _error;

    public bool CanStep { get; set; } = false;
    public bool NeedStep { get; private set; } = false;
    public bool Grounded { get; private set; } = true;
    public float LegError => _error;


    private void Awake()
    {
        _transform = transform;
        _transform.SetParent(null);
    }

    private void Update()
    {
        _transform.LookAt(_transform.position +  Vector3.ProjectOnPlane(_target.position - _transform.position, Vector3.up));

        _debug = NeedStep;

        CheckStep();
        UpdateStepByError();

        if (!Grounded)
        {
            UpdateStepAction();
        }


    }

    private void UpdateStepByError()
    {
        if (_error > _errorZone * _errorZone)
        {
            _startPosition = _idealPoint.position + Vector3.ProjectOnPlane((_transform.position- _idealPoint.position).normalized * _errorZone, Vector3.up);
            _transform.position = _startPosition;
        }
    }

    private void UpdateStepAction()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            var factor = Mathf.SmoothStep(0, 1, Mathf.Clamp01(1f - _timer / _stepTime));

            _transform.position = Vector3.Lerp(_startPosition, _idealPoint.position + _stepPoint, factor) + Vector3.up * _stepHeight * Mathf.Sin(Mathf.PI * factor);

            if (_timer <= 0f)
            {
                _timer = -1f;
                Grounded = true;
            }

        }
    }

    private void CheckStep()
    {
        _error = (_idealPoint.position - _transform.position).sqrMagnitude;
        NeedStep = (_error > _maxZone * _maxZone);
    }

    public void StartNewStepAction()
    {
        if (Grounded == false)
            return;

        Grounded = false;
        _startPosition = _transform.position;
        _stepPoint = (_idealPoint.position - _startPosition).normalized * _stepZone;
        _timer = _stepTime;
    }

    private void OnDrawGizmos()
    {

#if UNITY_EDITOR

        if (_idealPoint == null)
            return;

        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireDisc(_idealPoint.transform.position, Vector3.up, _stepZone);
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(_idealPoint.transform.position, Vector3.up, _maxZone);
        UnityEditor.Handles.color = Color.magenta;
        UnityEditor.Handles.DrawWireDisc(_idealPoint.transform.position, Vector3.up, _errorZone);

#endif

    }
}
