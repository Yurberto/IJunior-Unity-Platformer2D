using UnityEngine;

[RequireComponent(typeof(Mover))]
public class EnemyMover : MonoBehaviour
{
    private Mover _mover;
    private Rotator _rotator;

    private Patroler _patroler;
    private Follower _follower;

    private bool _isFollow = false;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _patroler = GetComponent<Patroler>();
        _follower = GetComponent<Follower>();

        _rotator = GetComponentInChildren<Rotator>();
    }

    public void Move()
    {
        if (_follower == null && _patroler == null)
            return;

        Vector2 direction = ((_isFollow || _patroler == null) && _follower != null) ? _follower.GetDirection() : _patroler.GetDirection();

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
