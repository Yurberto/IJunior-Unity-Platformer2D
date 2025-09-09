using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Follower : MonoBehaviour
{
    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public Vector2 GetDirection()
    {
        if (_target == null)
            return Vector2.zero;

        return _target.position - transform.position;
    }
}
