using System;
using System.Collections;
using UnityEngine;

public abstract class Resources : MonoBehaviour
{
    [SerializeField, Range(50, 10000)] protected float _maxValue = 100;
    [SerializeField, Range(0, 100)] protected float _currentValue = 100;

    public event Action ValueChanged;
    public event Action IsOver;

    public float MaxValue => _maxValue;
    public float CurrentValue => _currentValue;
    public bool IsFull => _currentValue >= _maxValue;

    protected virtual void OnValidate()
    {
        _currentValue = Mathf.Clamp(_currentValue, 0, _maxValue);
        ValueChanged?.Invoke();

        CheckIsOver();
    }

    protected void Increase(float increaseAmount)
    {
        if (increaseAmount < 0)
            return;

        _currentValue = Mathf.Min(_currentValue + increaseAmount, _maxValue);
        ValueChanged?.Invoke();

        CheckIsOver();
    }

    protected void Decrease(float decreaseAmount)
    {
        if (decreaseAmount < 0)
            return;

        _currentValue = Mathf.Max(_currentValue - decreaseAmount, 0);
        ValueChanged?.Invoke();

        CheckIsOver();
    }

    protected virtual void CheckIsOver()
    {
        if (_currentValue <= 0)
            IsOver?.Invoke();
    }
}
