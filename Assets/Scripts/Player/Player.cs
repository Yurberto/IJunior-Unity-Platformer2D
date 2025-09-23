using System;
using UnityEngine;

[RequireComponent(typeof(ItemCollector), typeof(DamageableDetector), typeof(Mover))]
[RequireComponent(typeof(Jumper), typeof(Health), typeof(Attacker))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private ItemCollector _collector;

    private PlayerAnimator _animator;
    private Mover _mover;
    private Jumper _jumper;

    private Health _health;
    private Attacker _attacker;

    private DamageableDetector _damageableDetector;

    public event Action Attacked;

    private void Awake()
    {
        _collector = GetComponent<ItemCollector>();

        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();

        _health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();
        _damageableDetector = GetComponent<DamageableDetector>();

        _animator = GetComponentInChildren<PlayerAnimator>();
    }

    private void OnEnable()
    {
        _inputReader.MovementKeyPressed += Move;
        _inputReader.MovementKeyReleased += StopMove;
        _inputReader.JumpKeyPressed += Jump;
        _inputReader.AttackKeyPressed += TryAttack;
        _collector.KitPickedUp += UseKit;
    }

    private void OnDisable()
    {
        _inputReader.MovementKeyPressed -= Move;
        _inputReader.MovementKeyReleased -= StopMove;
        _inputReader.JumpKeyPressed -= Jump;
        _inputReader.AttackKeyPressed -= TryAttack;
        _collector.KitPickedUp -= UseKit;
    }

    private void Move(float direction)
    {
        _mover.Move(direction);
        _animator.Move(direction);
    }

    private void StopMove()
    {
        _mover.StopMove();
        _animator.StopMove();
    }

    private void Jump()
    {
        if(_jumper.TryJump())
            _animator.Jump();
    }

    private void TryAttack()
    {
        if (_attacker == null || _attacker.IsAttack || _damageableDetector == null) 
            return;

        if (_damageableDetector.TryDetect(out IDamageable detected, _attacker.AttackRange))
        {
            _attacker.Attack(detected);
            _animator.Attack();
            _attacker.StartDelayCoroutine();
        }

        Attacked?.Invoke();
    }

    private void UseKit(Kit kit)
    {
        if (_health == null)
            return;

        _health.Heal(kit.HeelAmount);
    }
}