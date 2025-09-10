using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class EnemyFight : MonoBehaviour
{
    private TargetDetector _targetDetector;
    private DamageableDetector _damageableDetector;
    private Attacker _attacker;

    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _targetDetector = GetComponent<TargetDetector>();
        _damageableDetector = GetComponent<DamageableDetector>();
    }

    private void StartAttack()
    {
        
    }

    private void StopAttack()
    {
        StopCoroutine(_attackCoroutine);
    }

}
