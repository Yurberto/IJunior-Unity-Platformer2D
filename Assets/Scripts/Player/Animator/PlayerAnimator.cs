using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rotator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Animator _animator;
    private Rotator _rotator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rotator = GetComponent<Rotator>();
    }

    private void OnEnable()
    {
        _inputReader.MovementKeyPressed += Move;
        _inputReader.MovementKeyReleased += StopMove;
    }

    private void OnDisable()
    {
        _inputReader.MovementKeyPressed -= Move;
        _inputReader.MovementKeyReleased -= StopMove;
    }

    private void Move(float direction)
    {
        _rotator.LookAt(direction);
        _animator.SetBool(PlayerAnimatorData.Params.IsRun, true);
    }

    private void StopMove()
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsRun, false);
    }
}
