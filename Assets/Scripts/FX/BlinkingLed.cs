using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLed : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float _delay;
    [SerializeField] private float _lightTime;

    private Material _startMaterial;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _startMaterial = _renderer.sharedMaterial;
    }

    private IEnumerator Start()
    {
        yield return null;

        while (true)
        {
            _renderer.sharedMaterial = _startMaterial;
            yield return new WaitForSeconds(_delay);
            _renderer.sharedMaterial = _material;
            yield return new WaitForSeconds(_lightTime);
        }
    }


}
