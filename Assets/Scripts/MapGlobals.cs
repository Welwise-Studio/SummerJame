using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGlobals : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AimingSystem _aimingSystem;

    private static MapGlobals _instance;

    public Player Player => _player;
    public AimingSystem AimingSystem => _aimingSystem;

    public static MapGlobals Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MapGlobals>();

            return _instance;
        }
    }

    internal void OnPlayerDead()
    {
      //  throw new NotImplementedException();
    }
}
