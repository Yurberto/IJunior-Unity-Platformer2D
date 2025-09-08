using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _moveSpeed = 1;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        float speedMultiplier = 500;
        _rigidbody.velocity = new Vector2(direction.Normilize() * Time.fixedDeltaTime * _moveSpeed * speedMultiplier, _rigidbody.velocity.y);
    }

    public void StopMove()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }
}
