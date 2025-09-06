using UnityEngine;

[RequireComponent (typeof(Mover), typeof(GroundDetector))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Mover _mover;
    private GroundDetector _groundDetector;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
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
        {
            _mover.Jump();
            Debug.Log("прыгнуло");
        }
        else
            Debug.Log("не пригнуло\t" + _groundDetector.IsGround.ToString());
    }
}
