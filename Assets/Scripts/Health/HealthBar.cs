using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthImage;
    private UnitBehaviour _player;
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<UnitBehaviour>();
        _player.OnHealthChange += UpdateHealth;
    }

    private void UpdateHealth()
    {
        _healthImage.fillAmount = _player.Health / _player.MaxHealth;
    }
}
