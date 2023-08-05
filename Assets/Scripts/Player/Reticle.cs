using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField] private float _offset = .1f;

    private Vector3 _reticlePosition;
    private Vector3 _reticleNormal;

    private void Update()
    {
        HandleReticle();
        MoveReticle();
    }
    private void HandleReticle()
    {
        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out var hit))
        {
            _reticlePosition = hit.point;
            _reticleNormal = hit.normal;
        }
    }

    private void MoveReticle()
    {
        if (_reticleNormal != Vector3.up)
            return;

        transform.position = _reticlePosition + Vector3.up * _offset;
    }
}
