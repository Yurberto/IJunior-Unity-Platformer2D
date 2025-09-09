using UnityEngine;

[RequireComponent (typeof(CircleCollider2D))]
public class DamageableDetector : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float _range = 2;

    private CircleCollider2D _collider;
    private bool _hasTarget = false;

    public bool HasTarget => _hasTarget;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnValidate()
    {
        _collider.radius = _range;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Health health))
        {
            _hasTarget = true;
        }
    }
}
