using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(EnemyFighter), typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private TargetDetector _targetDetector;

    private Health _health;

    private EnemyMover _mover;
    private EnemyFighter _fighter;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _fighter = GetComponent<EnemyFighter>();
        _health = GetComponent<Health>();

        gameObject.layer = LayerMask.NameToLayer(LayerMaskData.InString.Enemy);
    }

    private void OnEnable()
    {
        _targetDetector.PlayerDetected += StartFollow;
        _targetDetector.PlayerLost += StopFollow;
        _targetDetector.DamageableDetected += StartAttack;
        _targetDetector.DamageableLost += StopAttack;

        _health.IsOver += Die;
    }

    private void OnDisable()
    {
        _targetDetector.PlayerDetected -= StartFollow;
        _targetDetector.PlayerLost -= StopFollow;
        _targetDetector.DamageableDetected -= StartAttack;
        _targetDetector.DamageableLost -= StopAttack;

        _health.IsOver += Die;
    }

    private void FixedUpdate()
    {
        _mover.Move();
    }

    private void StartFollow(Player player)
    {
        _mover.StartFollow(player);
    }

    private void StopFollow()
    {
        _mover.StopFollow();
    }

    private void StartAttack()
    {
        _fighter.StartAttack();
    }

    private void StopAttack()
    {
        _fighter.StopAttack();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
