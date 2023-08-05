using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Grenade : MonoBehaviour
{
    [Range(0.2f, 2f)]
    [SerializeField] private float _radius = 1f;

    [Range(5, 50)]
    [SerializeField] private int _damage = 30;

    [Range(0.5f, 10f)]
    [SerializeField] private float _timeBeforeExplosion = 1f;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private const int _maxCollidersCount = 10;
    private float _spawnedTime;

    private void OnEnable()
    {
        _spawnedTime = Time.time;
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (Time.time < _spawnedTime + _timeBeforeExplosion)
            yield return null;

        Explode();
        yield break;

    }
    public void Explode()
    {
        //TODO: Show explode effect
        Collider[] colliders = new Collider[_maxCollidersCount];
        int collidersCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, colliders);

        for (int i = 0; i < collidersCount; i++)
            if (colliders[i].TryGetComponent<IDamageable>(out var damageableObject))
                damageableObject.TakeDamage(_damage);

        gameObject.SetActive(false);
    }

    public void AddForce(Transform targetPos)
    {
        _rigidbody.ThrowTo(targetPos.position, ThrowHelper.HeightFromTime((targetPos.position - transform.position).magnitude / _speed, 9.81f));
    }

}
    