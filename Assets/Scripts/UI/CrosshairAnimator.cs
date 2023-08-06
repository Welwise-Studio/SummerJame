using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CrosshairAnimator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private Sprite[] _frames;

    private RectTransform _rect;
    private Image _image;
    private float _timer;
    private int _counter;

    private void Awake()
    {
        _rect = transform as RectTransform;
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _rect.Rotate(Vector3.forward, _rotationSpeed * Time.unscaledDeltaTime);

        _timer -= Time.unscaledDeltaTime * _animationSpeed;
        if (_timer < 0)
        {
            _timer = 1;
            _counter++;
            if (_counter >= _frames.Length)
                _counter = 0;
            _image.sprite = _frames[_counter];
        }
    }
}
