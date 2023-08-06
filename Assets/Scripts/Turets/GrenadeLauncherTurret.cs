using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherTurret : Turret
{
    [SerializeField] protected Grenade _projectale;
    [SerializeField] protected Transform _projectaleSpawn;
    
    ObjectPool<Grenade> _greenades;

    void Awake()
    {
        _greenades = new ObjectPool<Grenade>(1, _projectale);
        print(_greenades);
    }

    public override void Shoot()
    {
        Grenade grenade = _greenades.Spawn();
        grenade.transform.position = _projectaleSpawn.position;
        grenade.transform.rotation = _projectaleSpawn.rotation;
    } 
}
