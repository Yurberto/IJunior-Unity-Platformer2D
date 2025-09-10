using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField, Range(50, 100)] private float _maxHitPoints = 100;
    [SerializeField, Range(0, 100)] private float _hitPoints = 100;

    private void OnValidate()
    {
        _hitPoints = Mathf.Clamp(_hitPoints, 0, _maxHitPoints);
    }

    public void Heel(float heelAmount)
    {
        if (heelAmount < 0)
            return;

        _hitPoints = Mathf.Min(_hitPoints + heelAmount, _maxHitPoints);
    }

    public void TakeDamage(float damage)
    {
        _hitPoints -= Mathf.Clamp(damage, 0, _hitPoints);
    }
}
