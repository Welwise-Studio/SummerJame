using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherTurret : Turet
{
    [SerializeField] protected Grenade _projectale;
    [SerializeField] protected Transform _projectaleSpawn;

    ObjectPool<Grenade> _greenades;

    protected override void Awake()
    {
        base.Awake();

        _greenades = new ObjectPool<Grenade>(1, _projectale);
        print(_greenades);
    }

    protected override void Shoot(Transform _targetPos)
    {
        Grenade grenade = _greenades.Spawn();
        grenade.transform.position = _projectaleSpawn.position;
        grenade.transform.rotation = _projectaleSpawn.rotation;

        grenade.AddForce(_targetPos);
    } 
}
