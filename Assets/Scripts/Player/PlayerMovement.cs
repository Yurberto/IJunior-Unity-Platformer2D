using UnityEngine;

[RequireComponent (typeof(Mover), typeof(Jumper), typeof(GroundDetector))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Mover _mover;
    private Jumper _jumper;
    private GroundDetector _groundDetector;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _groundDetector = GetComponent<GroundDetector>();
    }

    private void OnEnable()
    {
        _inputReader.MovementKeyPressed += _mover.Move;
        _inputReader.MovementKeyReleased += _mover.StopMove;
        _inputReader.JumpKeyPressed += HandleJump;
    }

    private void OnDisable()
    {
        _inputReader.MovementKeyPressed -= _mover.Move;
        _inputReader.MovementKeyReleased -= _mover.StopMove;
        _inputReader.JumpKeyPressed -= HandleJump;
    }

    private void HandleJump()
    {
        if (_groundDetector.IsGround)
            _jumper.Jump();
    }
}
