using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingSystem : MonoBehaviour
{
    [SerializeField] private float _planeOffset;
    private Vector3 _aimPoint;
    private Plane _plane;
    private Camera _camera;

    public Vector3 AimPoint => _aimPoint;

    private void Awake()
    {
        _plane = new Plane(Vector3.up, Vector3.up * _planeOffset);
        _camera = Camera.main;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdatePositionByRaycast(Input.mousePosition);
    }

    public void UpdatePositionByRaycast(Vector3 screenPoint)
    {
        Ray ray = _camera.ScreenPointToRay(screenPoint);

        if (_plane.Raycast(ray, out float enter))
        {
            _aimPoint = ray.GetPoint(enter);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.up * _planeOffset, Vector3.right + Vector3.forward);
        Gizmos.DrawWireSphere(_aimPoint, 0.2f);
    }
}
