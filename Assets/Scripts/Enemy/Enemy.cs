using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    private Transform[] _waypoints;
    private Mover _mover;

    private int _currentWaypoint;

    public void Initialize(Transform[] waypoints)
    {
        _waypoints = waypoints;
    }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;

        _mover.Move(_waypoints[_currentWaypoint].position);
    }
}
