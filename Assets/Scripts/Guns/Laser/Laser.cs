using System.Collections;
using UnityEngine;
using VolumetricLines;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private VolumetricLineBehavior _lineBehaviour;
    [SerializeField] private float _preparingDuration = 1f;
    [SerializeField] private float _shootingDuration = 3f;
    [SerializeField] private float _reloadingDuration = 2f;
    [SerializeField] private int _damage = 1;

    private float _startPreparingTime;
    private float _startShootingTIme;
    private float _startReloadingTime;
    private Ray _ray;

    private void OnValidate()
    {
        _lineBehaviour.EndPos = new Vector3(0, 0, _distance);
    }

    private void Start()
    {
        _startPreparingTime = Time.time;
        StartCoroutine(PrepareToShoot());
    }

    private IEnumerator PrepareToShoot()
    {
        while(Time.time < _startPreparingTime + _preparingDuration && _lineBehaviour.LineWidth < 1f)
        {
            _lineBehaviour.LineWidth += 0.003f;
            yield return null;
        }

        while (_lineBehaviour.LineWidth > 0)
        {
            _lineBehaviour.LineWidth -= 0.001f;
            yield return null;
        }

        _startShootingTIme = Time.time;
        StartCoroutine(Shoot());
        yield break;
    }

    private IEnumerator Shoot()
    {
        while (_lineBehaviour.LightSaberFactor > 0.6)
        {
            _lineBehaviour.LightSaberFactor -= 0.01f;
            _lineBehaviour.LineWidth += 0.02f;
            yield return null;
        }

        while (Time.time < _startShootingTIme + _shootingDuration)
        {
            RaycastHit[] hits = new RaycastHit[5];
            _ray = new Ray(transform.position, transform.forward);
            int hitsCount = Physics.RaycastNonAlloc(_ray, hits, _distance);

            for (int i = 0; i < hitsCount; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_damage);
            }
            yield return null;
        }


        _startReloadingTime = Time.time;
        StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        while (_lineBehaviour.LineWidth >= 0)
        {
            _lineBehaviour.LineWidth -= 0.002f;
            yield return null;
        }

        while (Time.time < _startReloadingTime + _reloadingDuration)
            yield return null;

        _lineBehaviour.LightSaberFactor = 1f;
        _startPreparingTime = Time.time;
        StartCoroutine(PrepareToShoot());
        yield break;
    }
}