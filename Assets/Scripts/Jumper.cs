using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _jumpForce = 2.5f;
    [SerializeField] private LayerMask _groundLayer;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void Jump()
    {
        if (IsGrounded())
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        float acceptableDistance = 0.1f;
        RaycastHit2D hitted = Physics2D.Raycast(_collider.bounds.min, Vector2.down, acceptableDistance);

        return hitted.collider != null;
    }
}
