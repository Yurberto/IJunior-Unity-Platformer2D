using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Jumper), typeof(Mover))]
public class Player : MonoBehaviour
{
    [SerializeField] InputHandler _inputHandler;

    private Mover _mover;
    private Jumper _jumper;
    private Animator _animator;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputHandler.MovementKeyPressed += Run;
        _inputHandler.MovementStopped += StopRunning;
        _inputHandler.JumpKeyPressed += Jump;
    }

    private void OnDisable()
    {
        _inputHandler.MovementKeyPressed -= Run;
        _inputHandler.MovementStopped -= StopRunning;
        _inputHandler.JumpKeyPressed -= Jump;
    }

    private void Jump()
    {
        _jumper.Jump();
    }

    private void Run(Vector2 offset)
    {
        _mover.Move(offset);
        _animator.SetBool(PlayerAnimatorData.Params.IsRunning, true);
    }

    private void StopRunning()
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsRunning, false);
    }
}
