using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _damage = 10;
    [SerializeField, Range(0, 10)] private float _attackRange = 3f;
    [SerializeField, Range(0, 10)] private float _attackSpeed = 3f;

    private bool _isAttack = false;

    public float AttackRange => _attackRange;
    public bool IsAttack => _isAttack;

    public void Attack(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);
        Debug.Log("Attacked");
        StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        _isAttack = true;
        yield return new WaitForSeconds(1 / _attackSpeed);
        _isAttack = false;
    }
}
