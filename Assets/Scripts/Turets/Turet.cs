using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public abstract class Turet : MonoBehaviour
{
    [SerializeField] protected float _range;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _rotationSpeed;
    [SerializeField] protected GameObject _projectale;
    [SerializeField] protected Transform _projectaleSpawn;

    protected bool _attached = false; // взяли ли мы турель под контроль
    public bool Attached { get { return _attached; } set { _attached = value; } }
    protected bool _detected;

    protected Transform _target = null;
    protected Camera _camera;

    protected float _nextTimeToFire;

    protected abstract void Shoot();

    protected virtual void Awake()
    {
        _camera = Camera.main;
        _nextTimeToFire = 0;
    }

    protected virtual void Update()
    {
        if(_attached)
        {
            MouseTarget();
        }
        else
        {
            FindTarget();
        }
    }

    protected void RotateToTarget()
    {
        transform.up = Vector3.MoveTowards(transform.up, _target.transform.position, _rotationSpeed * Time.deltaTime);
    }

    protected void FindTarget()
    {
         RaycastHit ray;
        if(Physics.SphereCast(transform.position, _range, Vector3.forward, out ray))
        {
            if(ray.collider.gameObject.tag == "Player")
            {
                if(!_detected)
                {
                    _detected = true;
                    _target = ray.collider.gameObject.transform;
                }
            }else
            {
                if(_detected)
                {
                    _detected = false;
                    _target = null;
                }

            }
        }

        if (_detected)
        {
            RotateToTarget();
            if(Time.time > _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1 / _fireRate;
                Shoot();
            }
        }
    }

    protected void MouseTarget()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        mousePos.x = 0;
        transform.up = Vector3.MoveTowards(transform.up, mousePos, _rotationSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    protected void Attaching(Transform target) 
    {
        
    }
}
