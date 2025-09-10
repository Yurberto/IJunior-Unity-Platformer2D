using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField, Range(0, 1f)] private float _checkpointRadius = 1.0f;
    
    private int _currentPoint = 0;

    public Vector2 GetDirection()
    {
        if (transform.position.IsEnoughClose(_patrolPoints[_currentPoint].position, _checkpointRadius))
            _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;

        return _patrolPoints[_currentPoint].position - transform.position;
    }
}
