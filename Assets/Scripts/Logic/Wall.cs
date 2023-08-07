using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float _minDistance = 2f;
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private bool _isVertical;

    private const float _enabledHeight = 0f, _disabledHeight = -1.8f;
    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = MapGlobals.Instance.Player.transform;
    }
    private void Update()
    {
        float distance = GetDistance(_playerTransform);
        if (distance < 0)
            return;

        if (distance < _minDistance)
            SetHeight(_disabledHeight);
        else
            SetHeight(_enabledHeight);


    }


    private void SetHeight(float value)
    {
       transform.position = new Vector3(transform.position.x, value, transform.position.z);
    }

    private float GetDistance(Transform target)
    {
        if (_isVertical)
            return target.position.z - transform.position.z;
        else
            return target.position.x - transform.position.x;
    }
}
