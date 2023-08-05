using System;
using UnityEngine;

public abstract class UnitBehaviour : MonoBehaviour, IDamageable
{
    #region Public Fields
    public Action OnHealthChange;
    public Action OnDie;

    public float Health { get => _currentHealth; }
    public float MaxHealth { get => _maxHealth; }
    public float MinHealth { get => _minHealth; }
    #endregion

    #region Editor Fields
    [Header("Units Stats")]
    [SerializeField] private float _startHealth = 100;
    [Space(10)]
    [SerializeField] protected float _maxHealth = 100;
    [SerializeField] protected float _minHealth = 0;
    #endregion

    protected float _currentHealth;

    #region Unity Methods
    protected virtual void Awake()
    {
        SetHealth(_startHealth);
    }
    #endregion

    #region public Methods
    public virtual void Die() 
    {
        Destroy(gameObject);
    }

    public void SetHealth(float newHealth)
    {
        if (newHealth <= _maxHealth)
        {
            _currentHealth = newHealth;
        }
        else
            _currentHealth = _maxHealth;

        OnHealthChange?.Invoke();
    }

    public void AddHealth(float amount)
    {
        var predict = _currentHealth + amount;

        if (predict >= _maxHealth)
            return;

        _currentHealth = predict;
        OnHealthChange?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        var predict = _currentHealth - damage;
        if (predict <= _minHealth)
        {
            Die();
            OnDie?.Invoke();
            _currentHealth = _minHealth;
            return;
        }

        _currentHealth = predict;
        OnHealthChange?.Invoke();
    }
    #endregion
}
