using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WorldLine : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Transform _transform;
    private LineRenderer _renderer;

    private void Awake()
    {
        _transform = transform;
        _renderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        _renderer.SetPosition(0, _transform.position);
        _renderer.SetPosition(1, _target.position);
    }
}
