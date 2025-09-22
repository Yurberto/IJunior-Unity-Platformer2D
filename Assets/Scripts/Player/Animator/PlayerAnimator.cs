using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rotator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rotator _rotator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rotator = GetComponent<Rotator>();
    }

    public void Move(float direction)
    {
        _rotator.LookAt(direction);
        _animator.SetBool(PlayerAnimatorData.Params.IsRun, true);
    }

    public void StopMove()
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsRun, false);
    }

    public void Jump()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    public void Attack()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Attack);
    }
}
