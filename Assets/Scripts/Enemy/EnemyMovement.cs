using UnityEngine;

[RequireComponent(typeof(Patrol), typeof(Follower), typeof(Mover))]
[RequireComponent(typeof(Rotator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private TargetDetector _targetDetector;

    private Mover _mover;
    private Rotator _rotator;

    private Patrol _patrol;
    private Follower _follower;

    private bool _isFollow = false;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _patrol = GetComponent<Patrol>();
        _follower = GetComponent<Follower>();
    }

    private void OnEnable()
    {
        _targetDetector.PlayerDetected += StartFollow;
        _targetDetector.PlayerLost += () => _isFollow = false;
    }

    private void OnDisable()
    {
        _targetDetector.PlayerDetected -= StartFollow;
        _targetDetector.PlayerLost -= () => _isFollow = false;
    }

    private void FixedUpdate()
    {
        Vector2 direction = _isFollow ? _follower.GetDirection() : _patrol.GetDirection();

        _mover.Move(direction.x);
        _rotator.LookAt(direction.x);
    }

    private void StartFollow(Player player)
    {
        _isFollow = true;
        _follower.SetTarget(player.transform);
    }
}
