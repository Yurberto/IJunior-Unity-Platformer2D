using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _damage = 10;

    public void Attack(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);
    }
}
