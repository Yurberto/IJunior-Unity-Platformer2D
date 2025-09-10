using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(Collider2D))]
public class DamageableDetector : MonoBehaviour
{
    [SerializeField, Range(0.01f, 10f)] private float _offset = 0.01f;

    private Collider2D _characterCollider;

    private void Awake()
    {
        _characterCollider = GetComponent<Collider2D>();
    }

    public bool TryDetect(out IDamageable detected, float detectRange)
    {
        float direction = Mathf.Sign(transform.localScale.x);

        float boxCenterX = direction > 0 ? _characterCollider.bounds.max.x + detectRange / 2 + _offset : _characterCollider.bounds.min.x - detectRange / 2 - _offset;

        Vector2 boxCenter = new Vector2(boxCenterX, _characterCollider.bounds.center.y);
        Vector2 boxSize = new Vector2(detectRange, _characterCollider.bounds.max.y - _characterCollider.bounds.min.y);

        Collider2D[] hitted = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);

        foreach (Collider2D hit in hitted)
        {
            if (hit.TryGetComponent(out Health health))
            {
                detected = health;
                return true;
            }
        }

        detected = null;
        return false;
    }
}
