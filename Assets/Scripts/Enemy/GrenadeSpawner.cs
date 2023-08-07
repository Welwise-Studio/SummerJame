using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GrenadeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnSpeed = 3;
    [SerializeField] private Grenade _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _target;

    private float _lastSpawnTime;

    private void Start()
    {
        _lastSpawnTime = Time.time;
    }

    void FixedUpdate()
    {
        if(Time.time > _lastSpawnTime + _spawnSpeed) 
        {
            Grenade grenade = Instantiate(_prefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
           // grenade.AddForce(_target);
            _lastSpawnTime = Time.time;
        }
    }
}
