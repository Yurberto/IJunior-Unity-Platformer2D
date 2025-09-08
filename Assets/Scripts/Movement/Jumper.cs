using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _jumpForce = 8;

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
}
