using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(GroundDetector))]
public class Mover : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _moveSpeed = 1;
    [SerializeField, Range(0, 10)] float _jumpForce = 8;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }

    public void Move(float direction)
    {
        float speedMultiplier = 500;
        _rigidbody.velocity = new Vector2(direction * Time.fixedDeltaTime * _moveSpeed * speedMultiplier, _rigidbody.velocity.y);
    }

    public void StopMove()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }
}
