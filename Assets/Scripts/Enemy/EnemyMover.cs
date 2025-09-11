using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Rotator))]
public class EnemyMover : MonoBehaviour
{
    private Mover _mover;
    private Rotator _rotator;

    private Patroler _patrol;
    private Follower _follower;

    private bool _isFollow = false;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _patrol = GetComponent<Patroler>();
        _follower = GetComponent<Follower>();
    }

    public void Move()
    {
        if (_follower == null && _patrol == null)
            return;

        Vector2 direction = ((_isFollow || _patrol == null) && _follower != null) ? _follower.GetDirection() : _patrol.GetDirection();

        _mover.Move(direction.x);
        _rotator.LookAt(direction.x);
    }

    public void StartFollow(Player player)
    {
        if (_follower == null)
            return;

        _isFollow = true;
        _follower.SetTarget(player.transform);
    }

    public void StopFollow() => _isFollow = false;
}
