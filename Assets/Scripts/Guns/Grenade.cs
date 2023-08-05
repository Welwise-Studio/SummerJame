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

    private const int _maxCollidersCount = 10;
    private float _spawnedTime;
    private float g = Physics.gravity.y;

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
        Vector3 FromTo = targetPos.position - transform.position;
        Vector3 FromXZ = new Vector3(FromTo.x, 0f, FromTo.z);
        float x = FromTo.x; float y = FromTo.y;
        float angleInRadians = 10 * Mathf.PI / 100;
        float dir = (g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        var d = Mathf.Sqrt(Mathf.Abs(dir));

        _rigidbody.velocity = transform.forward * d;
    }

}
    