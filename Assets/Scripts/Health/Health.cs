using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField, Range(50, 10000)] private float _maxValue = 100;
    [SerializeField, Range(0, 100)] private float _currentValue = 100;

    public event Action ValueChanged;

    public float MaxValue => _maxValue;
    public float CurrentValue => _currentValue;

    private void OnValidate()
    {
        _currentValue = Mathf.Clamp(_currentValue, 0, _maxValue);
    }

    public void Heal(float healAmount)
    {
        if (healAmount < 0)
            return;

        _currentValue = Mathf.Min(_currentValue + healAmount, _maxValue);
        ValueChanged?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0) 
            return;

        _currentValue = Mathf.Max(_currentValue - damage, 0);
        ValueChanged?.Invoke();
    }
}
