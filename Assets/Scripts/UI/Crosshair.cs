using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private AimingSystem _aiming;

    private Camera _camera;
    private RectTransform _transform;
    private RectTransform _parent;

    private void Awake()
    {
        _camera = Camera.main;
        _transform = transform as RectTransform;
        _parent = _transform.parent as RectTransform;
    }

    private void Update()
    {
        Vector2 screenPoint = _camera.WorldToScreenPoint(_aiming.AimPoint);
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_parent, screenPoint, null, out Vector2 localPoint))
            _transform.localPosition = localPoint;
    }


}
