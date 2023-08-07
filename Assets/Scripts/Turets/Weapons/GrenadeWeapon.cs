using System;
using System.Collections;
using UnityEngine;

public class GrenadeWeapon : BaseWeapon
{

    [SerializeField] private float _fireRate;
    [SerializeField] private AudioSource _shoot;
    [SerializeField] private Grenade _prefab;

    private Transform _transform;
    private float _timer;

    public override void OnPlayerEquip()
    {
    }

    private void Awake()
    {
        _transform = transform;
        _timer = -1f;
        IsReady = true;
    }

    public override void Shoot()
    {
        if (IsReady)
        {
            _timer = 1f / _fireRate;
        }
    }


    private void Update()
    {
        IsReady = _timer < 0f;

        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                _timer = -1f;
                Fire();
            }
        }
    }

    private void Fire()
    {
        Grenade grenade = Instantiate(_prefab, _transform.position, Quaternion.identity);
        grenade.AddForce(_transform.position + _transform.forward * 2f);
        grenade.transform.position = _transform.position;
        _shoot.Play();
    }
}
