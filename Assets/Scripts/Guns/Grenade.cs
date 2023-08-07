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
    [SerializeField] private ParticleSystem _explosionPrefab;
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

    private void OnCollisionEnter(Collision collision)
    {
        _spawnedTime = Mathf.Min(_spawnedTime, Time.time + 0.3f);
        var component = collision.collider.GetComponentInParent<UnitBehaviour>();
        if (component!=null)
        {
            Explode();
        }
    }

    public void Explode()
    {

        Collider[] colliders = new Collider[_maxCollidersCount];
        int collidersCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, colliders);

        for (int i = 0; i < collidersCount; i++)
        {
            var component = colliders[i].GetComponentInParent<UnitBehaviour>();
            component?.TakeDamage(_damage);
        }

        gameObject.SetActive(false);
        ShowExplosion();
    }

    private void ShowExplosion()
    {
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = Vector3.one/2f;
        explosion.Play();
        Destroy(explosion.gameObject, 3f);
    }

    public void AddForce(Vector3 targetPos)
    {
        _rigidbody.ThrowTo(targetPos, 0.01f);
    }

}
    