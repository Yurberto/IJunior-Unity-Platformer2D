using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageableDetector : MonoBehaviour
{
    [SerializeField, Range(0.01f, 10f)] private float _offset = 0.01f;
    [SerializeField, Range(0, 20)] private int _maxColliders = 10;

    private Rotator _rotator;

    private Collider2D _characterCollider;
    private Collider2D[] _hitted;
    private int _collidersCount;

    private void Awake()
    {
        _rotator = GetComponentInChildren<Rotator>();

        _characterCollider = GetComponent<Collider2D>();
        _hitted = new Collider2D[_maxColliders];
    }

    public bool TryDetect(out IDamageable detected, float detectRange)
    {
        if (_rotator == null)
            throw new System.Exception("Rotator not assigned in children!!!");

        float direction = _rotator.LookDirection;

        float boxCenterX = direction > 0 ? _characterCollider.bounds.max.x + detectRange / 2 + _offset : _characterCollider.bounds.min.x - detectRange / 2 - _offset;

        Vector2 boxCenter = new Vector2(boxCenterX, _characterCollider.bounds.center.y);
        Vector2 boxSize = new Vector2(detectRange, _characterCollider.bounds.max.y - _characterCollider.bounds.min.y);
        
        _collidersCount = Physics2D.OverlapBoxNonAlloc(boxCenter, boxSize, 0f, _hitted);

        for (int i = 0; i < _collidersCount; i++)
        {
            if (_hitted[i] != _characterCollider && _hitted[i].TryGetComponent(out IDamageable health))
            {
                detected = health;
                return true;
            }
        }

        detected = null;
        return false;
    }
}
