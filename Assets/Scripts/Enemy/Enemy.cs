using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField, Range(0, 5)] private float _moveSpeed;

    private int _currentWaypoint = 0;

    private void Update()
    {
        if (_waypoints == null || _waypoints.Length <= 0)
            return;
        
        Move();
    }

    private void Move()
    {
        if (transform.position.x == _waypoints[_currentWaypoint].position.x)
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _moveSpeed * Time.deltaTime);
    }
}
