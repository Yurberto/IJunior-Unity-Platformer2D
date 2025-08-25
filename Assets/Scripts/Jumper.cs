using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _jumpForce = 2.5f;
    [SerializeField] private LayerMask _groundLayer;

    private bool _isGrounded = false;

    public void Jump()
    {
        CheckGrounded();
        Debug.Log("jump");
        if (_isGrounded)
        {
            Debug.Log(_isGrounded);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckGrounded()
    {
        Debug.Log("Check");
        float acceptableDistance = 1f;
        RaycastHit2D hitted = Physics2D.Raycast(transform.position, Vector2.down, acceptableDistance);
        _isGrounded = hitted.collider.gameObject.GetComponent<TilemapCollider2D>();
    }
}
