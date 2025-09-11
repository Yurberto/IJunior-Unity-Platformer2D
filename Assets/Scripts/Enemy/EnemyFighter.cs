using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attacker), typeof(DamageableDetector))]
public class EnemyFighter : MonoBehaviour
{
    private DamageableDetector _damageableDetector;
    private Attacker _attacker;

    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _damageableDetector = GetComponent<DamageableDetector>();
    }

    public void StartAttack()
    {
        _attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    public void StopAttack()
    {
        StopCoroutine(_attackCoroutine);
    }

    private IEnumerator AttackCoroutine()
    {
        var wait = new WaitWhile(() => _attacker.IsAttack);

        while (enabled)
        {
            yield return wait;

            if (_damageableDetector.TryDetect(out IDamageable detected, _attacker.AttackRange))
            {
                _attacker.Attack(detected);
                _attacker.StartDelayCoroutine();
            }
        }
    }
}
