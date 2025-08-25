using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(Mover))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputHandler _inputHandler;

    private Mover _mover;
    private Jumper _jumper;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
    }

    private void OnEnable()
    {
        _inputHandler.MovementKeyPressed += _mover.Move;
        _inputHandler.JumpKeyPressed += _jumper.Jump;
    }

    private void OnDisable()
    {
        _inputHandler.MovementKeyPressed -= _mover.Move;
        _inputHandler.JumpKeyPressed += _jumper.Jump;
    }
}
