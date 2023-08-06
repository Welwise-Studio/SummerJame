using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunTurret : Turret
{
    [SerializeField] protected Bullet _projectale;
    [SerializeField] protected Transform _projectaleSpawn;

    ObjectPool<Bullet> _bullets;

    void Awake()
    {

        _bullets = new ObjectPool<Bullet>(1, _projectale);
        print(_bullets);
    }

    public override void Shoot()
    {
        Bullet bullet = _bullets.Spawn();
        bullet.transform.position = _projectaleSpawn.position;
        bullet.transform.eulerAngles = new Vector3(90f, _projectaleSpawn.parent.eulerAngles.y, 0f);
    }
}
