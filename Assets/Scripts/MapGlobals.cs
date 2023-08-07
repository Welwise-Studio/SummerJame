using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGlobals : MonoBehaviour
{
    public ObjectPool<Bullet> EnemyBullets;
    public ObjectPool<Bullet> PlayerBullets;

    [SerializeField] private Player _player;
    [SerializeField] private AimingSystem _aimingSystem;
    [SerializeField] private Bullet _playerBullet;
    [SerializeField] private Bullet _enemyBullet;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _gameOverSound;
    [SerializeField] private GameObject _firePrefab;

    private static MapGlobals _instance;

    public Player Player => _player;
    public AimingSystem AimingSystem => _aimingSystem;

    public static MapGlobals Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapGlobals>();
                _instance.Prepare();
            }

            return _instance;
        }
    }

    private void Prepare()
    {
        EnemyBullets = new ObjectPool<Bullet>(10, _enemyBullet);
        PlayerBullets = new ObjectPool<Bullet>(10, _playerBullet);
    }

    internal void OnPlayerDead()
    {
        _music.Stop();
        _gameOverSound.Play();
        Instantiate(_firePrefab, _player.transform.position, Quaternion.identity);
    }
}
