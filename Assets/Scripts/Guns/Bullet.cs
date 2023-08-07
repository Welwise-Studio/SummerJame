using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    [SerializeField] private Rigidbody _rigidbody;

    public void Hitting(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<UnitBehaviour>(out var damageableObject))
            damageableObject.TakeDamage(_damage);

        gameObject.SetActive(false);
        _rigidbody.freezeRotation = false;
    } 

    private void OnCollisionEnter(Collision collision)
    {
        Hitting(collision);
    }

    public void SetSpeed(Vector3 speed)
    {
        _rigidbody.velocity = speed;
        _rigidbody.freezeRotation = true;
    }
}
