using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private List<Transform> _wall;

    private const float _minDistance = 1f;
    private const float _enabledHeight = 1.15f, _disabledHeight = -0.7f;
    private Transform _playerTransform;
    private Vector3 _forward;

    private void Awake()
    {
        _playerTransform = MapGlobals.Instance.Player.transform;
        _forward = transform.forward;

    }
    private void Update()
    {
        float distance = 100;

        if (distance < _minDistance)
            SetHeight(_disabledHeight);
        else
            SetHeight(_enabledHeight);


    }

    private void SetHeight(float value)
    {
        _wall.ForEach(item => item.position.Set(item.position.x, value, item.position.z));
    }
}
