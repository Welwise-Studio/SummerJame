using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool;

    private T _prefab;
    private Action<T> _factoryAction;
    public ObjectPool(int size, T prefab, Action<T> factory = null)
    {
        _prefab = prefab;
        _factoryAction = factory;
        _pool = new List<T>(size);

        for (int i = 0; i < size; i++)
        {
            _pool[i] = CreateObject();
        }
    }

    #region Public Methods
    public T Spawn()
    {
        foreach (var mono in _pool)
        {
            if (!mono.gameObject.activeSelf)
            {
                mono.gameObject.SetActive(true);
                return mono;
            }
        }

        var newMono  = CreateObject();
        _pool.Add(newMono);

        newMono.gameObject.SetActive(true);
        return newMono;
    }

    public T Spawn(T toSpawn)
    {
        foreach (var mono in _pool)
        {
            if (!mono.gameObject.activeSelf)
            {
                mono.gameObject.SetActive(true);
                return mono;
            }
        }

        var newMono = CreateObject(toSpawn);
        _pool.Add(newMono);

        newMono.gameObject.SetActive(true);
        return newMono;
    }

    public void Despawn(T toDespawn)
    {
        foreach (var mono in _pool)
        {
            if (mono == toDespawn)
            {
                mono.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    private T CreateObject(T prefab)
    {
        var mono = GameObject.Instantiate<T>(prefab);
        _factoryAction?.Invoke(mono);
        mono.gameObject.SetActive(false);
        return mono;
    }

    private T CreateObject()
    {
        var mono = GameObject.Instantiate<T>(_prefab);
        _factoryAction?.Invoke(mono);
        mono.gameObject.SetActive(false);
        return mono;
    }

}
