using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform _healthImage;
    [SerializeField] private UnitBehaviour _target;
    private float _startShift;

    private void Awake()
    {
        _startShift = _healthImage.anchoredPosition.x;
    }

    private void Update()
    {
        _healthImage.anchoredPosition = Vector3.right * (_target.Health / _target.MaxHealth) * _startShift;
    }
}
