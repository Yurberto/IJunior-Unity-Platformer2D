using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class EnemyFight : MonoBehaviour
{
    [SerializeField] private TargetDetector _targetDetector;

    private DamageableDetector _damageableDetector;
    private Attacker _attacker;

    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _damageableDetector = GetComponent<DamageableDetector>();
    }

    private void OnEnable()
    {
        if (_targetDetector == null)
            return;

        _targetDetector.DamageableDetected += StartAttack;
        _targetDetector.DamageableLost += StopAttack;
    }

    private void OnDisable()
    {
        if (_targetDetector == null)
            return;

        _targetDetector.DamageableDetected -= StartAttack;
        _targetDetector.DamageableLost -= StopAttack;
    }

    private void StartAttack()
    {
        _attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private void StopAttack()
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
