using UnityEngine;

public class Vitality : MonoBehaviour, IDamageable
{
    [SerializeField, Range(50, 100)] private float _maxHealth = 100;
    [SerializeField, Range(0, 100)] private float _health = 100;

    private void OnValidate()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

    public void Heel(float heelAmount)
    {
        if (heelAmount < 0)
            return;

        _health = Mathf.Min(_health + heelAmount, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _health -= Mathf.Clamp(damage, 0, _health);
        Debug.Log(name.ToString() + " - " + _health.ToString() + "hp");
    }
}
