using System;
using System.Collections;
using UnityEngine;

public class GrenadeWeapon : BaseWeapon
{
    [SerializeField] private float _reloadingDuration = 4f;
    [SerializeField] private AudioSource _shoot;
    [SerializeField] private Grenade _prefab;

    private Transform _transform;
    private float _lastShootingTime;

    public override void OnPlayerEquip()
    {
    }

    private void Awake()
    {
        _transform = transform;
        _lastShootingTime = -1f;
        IsReady = true;
    }

    private void Update()
    {
        if (Time.time > _lastShootingTime + _reloadingDuration && !IsReady)
            IsReady = true;
    }

    public override void Shoot()
    {
        Grenade grenade = Instantiate(_prefab, _transform.position, Quaternion.identity);
        grenade.AddForce(_transform.position + _transform.forward * 2f);
        grenade.transform.position = _transform.position;
        _shoot.Play();
        _lastShootingTime = Time.time;
        IsReady = false;
    }
}
