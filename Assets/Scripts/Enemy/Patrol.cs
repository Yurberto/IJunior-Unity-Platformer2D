using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField, Range(0, 1f)] private float _checkpointRadius = 1.0f;

    private Mover _mover;
    private Rotator _rotator;
    private int _currentPoint;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _patrolPoints[_currentPoint].position) < _checkpointRadius)
            _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;

        Vector2 direction = _patrolPoints[_currentPoint].position - transform.position;

        _mover.Move(direction.x);
        _rotator.LookAt(direction.x);
    }
}
