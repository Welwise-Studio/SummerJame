using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour
{
    [SerializeField] private Transform _door;
    [SerializeField] private float _closeTimerDuration;
    [Header("Animation")]
    [SerializeField] private float _animationTime;
    [SerializeField] private Vector3 _openPosition;
    [SerializeField] private Vector3 _closePosition;

    private bool _isOpen = false;
    private float _timer;

    private void Start()
    {
        Open();
    }

    private void Update()
    {
        if (_timer >= _closeTimerDuration && _isOpen)
            Close();
        else
            _timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.TryGetComponent<UnitBehaviour>(out var unit))
        {
            Open();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<UnitBehaviour>(out var unit))
        {
            _timer = 0;
        }
    }

    private void Open()
    {
        if (_isOpen)
            return;

        StopCoroutine(CloseRoutine());
        StartCoroutine(OpenRoutine());
        _isOpen = true;
    }

    private void Close()
    {
        if (!_isOpen)
            return;

        StopCoroutine(OpenRoutine());
        StartCoroutine(CloseRoutine());
        _isOpen = false;
    }


    public IEnumerator OpenRoutine()
    {
        _door.localPosition = _closePosition;
        for (float t = 0; t < _animationTime; t += Time.deltaTime)
        {
            _door.localPosition = Vector3.Lerp(_closePosition, _openPosition, t/_animationTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _door.localPosition = _openPosition;
        _isOpen = true;
    }

    public IEnumerator CloseRoutine() 
    {
        _door.localPosition = _openPosition;
        for (float t = 0; t < _animationTime; t += Time.deltaTime)
        {
            _door.localPosition = Vector3.Lerp(_openPosition, _closePosition, t / _animationTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _door.localPosition = _closePosition;
    }
}
