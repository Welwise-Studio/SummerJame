using UnityEngine;

public abstract class Turet : MonoBehaviour
{
    [SerializeField] protected float _range;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _rotationSpeed;

    protected bool _attached = false; // взяли ли мы турель под контроль
    public bool Attached { get { return _attached; } set { _attached = value; } }
    protected bool _detected;

    protected Transform _target = null;
    protected Camera _camera;

    protected float _nextTimeToFire;

    protected abstract void Shoot(Transform _targetPos);

    protected virtual void Awake()
    {
        _camera = Camera.main;
        _nextTimeToFire = 0;
    }

    protected virtual void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
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

    protected virtual void FixedUpdate()
    {
        if(_detected)
        {
            Vector3 dirPlayer = (_target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dirPlayer);
            lookRotation.x = 0f; lookRotation.z = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    protected void FindTarget()
    {
        if(Vector3.Distance(_target.position, transform.position) < _range)
        {
            if(!_detected)
            {
                _detected = true;
            }
        }else if(_detected)
        {
            _detected = false;
        }

        if (_detected)
        {
            if(Time.time > _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1 / _fireRate;
                Shoot(_target);
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
            Shoot(_target);
        }
    }

    protected void Attaching(Transform target) 
    {
        
    }
}
