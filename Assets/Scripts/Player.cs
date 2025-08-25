using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(Mover))]
public class Player : MonoBehaviour
{
    [SerializeField] InputHandler _inputHandler;

    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnEnable()
    {
        _inputHandler.MovementKeyPressed += _mover.Move;
    }

    private void OnDisable()
    {
        _inputHandler.MovementKeyPressed -= _mover.Move;
    }
}
