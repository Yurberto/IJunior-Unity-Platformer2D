using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [SerializeField] private TargetDetector _targetDetector;

    private EnemyMover _mover;
    private EnemyFighter _fighter;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _fighter = GetComponent<EnemyFighter>();
    }

    private void OnEnable()
    {
        _targetDetector.PlayerDetected += _mover.StartFollow;
        _targetDetector.PlayerLost += _mover.StopFollow;
        _targetDetector.DamageableDetected += _fighter.StartAttack;
        _targetDetector.DamageableLost += _fighter.StopAttack;
    }

    private void OnDisable()
    {
        _targetDetector.PlayerDetected -= _mover.StartFollow;
        _targetDetector.PlayerLost -= _mover.StopFollow;
        _targetDetector.DamageableDetected -= _fighter.StartAttack;
        _targetDetector.DamageableLost -= _fighter.StopAttack;
    }

    private void FixedUpdate()
    {
        if (_mover != null)
            _mover.Move();
    }
}
