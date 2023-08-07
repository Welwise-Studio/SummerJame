using System;
using UnityEngine;

public abstract class UnitBehaviour : MonoBehaviour
{
    #region Public Fields
    public Action OnHealthChange;
    public Action OnDie;

    public float Health { get => _currentHealth; }
    public float MaxHealth { get => _maxHealth; }
    public float MinHealth { get => _minHealth; }
    #endregion

    #region Editor Fields
    [SerializeField] private bool _drawGizmo;
    [Header("Units Stats")]
    [SerializeField] private float _startHealth = 100;
    [Space(10)]
    [SerializeField] protected float _maxHealth = 100;
    [SerializeField] protected float _minHealth = 0;
    [Header("Floating Text")]
    [SerializeField] private Transform _damageTextPoint;
    [SerializeField] private Vector3 _textArea = Vector3.one;
    [SerializeField] private DynamicTextData _damageTextData;
    #endregion

    protected float _currentHealth;
    bool _dead = false;

    #region Unity Methods
    protected virtual void Awake()
    {
        ResetHealth();

    }
    protected virtual void OnDrawGizmos()
    {
        if (!_drawGizmo)
            return;

        var tcolor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_damageTextPoint.position, _textArea * 2);
        Gizmos.color = tcolor;
    }
    #endregion

    #region public Methods
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        _dead = false;
        OnHealthChange?.Invoke();
    }

    public void AddHealth(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnHealthChange?.Invoke();
        _dead = false;
    }

    public void TakeDamage(float damage)
    {
        if (_dead)
            return;

        CreateDamageText(damage);

        var predict = _currentHealth - damage;
        if (predict <= _minHealth)
        {
            Die();
            OnDie?.Invoke();
            _dead = true;
            _currentHealth = _minHealth;
            return;
        }

        _currentHealth = predict;
        OnHealthChange?.Invoke();
    }
    #endregion

    private void CreateDamageText(float damage)
    {
        if (!_damageTextData)
            return;

        if (!_damageTextPoint)
            _damageTextPoint = transform;

        var newPos = _damageTextPoint.position +
            new Vector3(UnityEngine.Random.Range(-_textArea.x, _textArea.x),
            UnityEngine.Random.Range(-_textArea.y, _textArea.y),
            UnityEngine.Random.Range(-_textArea.z, _textArea.z));

        DynamicTextManager.CreateText(newPos, damage.ToString(), _damageTextData);
    }
}
