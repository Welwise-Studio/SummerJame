using System;
using System.Collections;
using UnityEngine;

public class BulletWeapon : BaseWeapon
{

    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private AudioSource _shoot;

    private Transform _transform;
    private float _timer;
    private ObjectPool<Bullet> _pool;

    public override void OnPlayerEquip()
    {
        _pool = MapGlobals.Instance.PlayerBullets;
    }

    private void Awake()
    {
        _transform = transform;
        _pool = MapGlobals.Instance.EnemyBullets;
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
        var bullet = _pool.Spawn();
        bullet.SetSpeed(_transform.forward * _bulletSpeed);
        bullet.transform.position = _transform.position;
        _shoot.Play();
    }
}
