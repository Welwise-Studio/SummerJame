using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserWeapon : BaseWeapon
{
    [SerializeField] private float _damage = 100f;
    [SerializeField] private float _predelay;
    [SerializeField] private float _fireTime;
    [SerializeField] private float _nextCycle;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private Material _materialForInstance;
    [SerializeField] private AudioSource _charge;
    [SerializeField] private AudioSource _shoot;
    [SerializeField] private VolumetricLines.VolumetricLineBehavior _line;
    [SerializeField] private float _lineWight;
    [SerializeField] private Color _colorEnemy, _colorPlayer;

    private Color _color;
    private Transform _transform;
    private bool _active = false;

    public override void OnPlayerEquip()
    {
        _color = _colorPlayer;
        _line.LineColor = _color;
    }

    private void Awake()
    {
        _color = _colorEnemy;
        _transform = transform;
        _materialForInstance = new Material(_materialForInstance);
        foreach (var renderer in _meshRenderers)
        {
            renderer.materials[1] = _materialForInstance;
        }
        IsReady = true;
    }

    public override void Shoot()
    {
        if (IsReady)
        {
            StopAllCoroutines();
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        IsReady = false;

        float time = 0;

        _charge.Play();
        while (time < _predelay)
        {
            time += Time.deltaTime;
            var color = _color * time / _predelay;
            color.r *= 5f;
            color.g *= 5f;
            color.b *= 5f;
            foreach (var renderer in _meshRenderers)
            {
                renderer.sharedMaterials[1].SetColor("_EmissionColor", color);
            }
            yield return null;
        }

        _shoot.Play();
        _active = true;
        _line.LineWidth = _lineWight;
        yield return new WaitForSeconds(_fireTime);
        _line.LineWidth = 0f;
        _active = false;
        foreach (var renderer in _meshRenderers)
        {
            renderer.sharedMaterials[1].SetColor("_EmissionColor", Color.black);
        }
        yield return new WaitForSeconds(_nextCycle);
        IsReady = true;
    }

    private void Update()
    {
        if (!_active)
            return;

        if (Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hit, 45f))
        {
            _line.SetStartAndEndPoints(Vector3.zero, Vector3.forward * hit.distance);
            TryToDamage(hit);
        }
        else
        {
            _line.SetStartAndEndPoints(Vector3.zero, Vector3.forward * 45f);
        }
    }

    private void TryToDamage(RaycastHit hit)
    {
        var health = hit.collider.GetComponentInParent<UnitBehaviour>();
        health?.TakeDamage(_damage * Time.deltaTime);
    }
}
