using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class GroundDetector : MonoBehaviour
{   
    [SerializeField, Range(0, 0.4f)] private float _maxDistanceDetect = 0.1f;

    private Collider2D _collider;
    private bool _isGround;

    public bool IsGround => _isGround;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        _isGround = ApplyIsGrounded();
    }

    private bool ApplyIsGrounded()
    {
        Vector2 minX = new Vector2(_collider.bounds.min.x, _collider.bounds.min.y);
        Vector2 maxX = new Vector2(_collider.bounds.max.x, _collider.bounds.min.y);

        Vector2[] downBounds = new Vector2[]
        {
            minX,
            maxX,
            _collider.bounds.min
        };

        foreach (Vector2 downBound in downBounds)
        {
            RaycastHit2D hit = Physics2D.Raycast(downBound, Vector2.down, _maxDistanceDetect);

            if (hit.collider != null && hit.collider.TryGetComponent(out Ground _))
                return true;
        }

        return false;
    }
}
