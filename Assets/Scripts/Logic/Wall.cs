using UnityEngine;

public class Wall : MonoBehaviour
{
    private const float _minDistance = 1f;
    private const float _enabledHeight = 0f, _disabledHeight = -1.8f;
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
       transform.position.Set(transform.position.x, value, transform.position.z);
    }
}
