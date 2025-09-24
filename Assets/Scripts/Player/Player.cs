using System;
using UnityEngine;

[RequireComponent(typeof(ItemCollector), typeof(DamageableDetector), typeof(Mover))]
[RequireComponent(typeof(Jumper), typeof(Health), typeof(Attacker))]
[RequireComponent (typeof(VampiricUltimateActivator), typeof(Mana))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private ItemCollector _collector;

    private PlayerAnimator _animator;
    private Mover _mover;
    private Jumper _jumper;

    private Health _health;
    private Mana _mana;

    private Attacker _attacker;
    private VampiricUltimateActivator _vampiricUltimateActivator;

    private DamageableDetector _damageableDetector;

    private void Awake()
    {
        _collector = GetComponent<ItemCollector>();

        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();

        _health = GetComponent<Health>();
        _mana = GetComponent<Mana>();

        _attacker = GetComponent<Attacker>();
        _vampiricUltimateActivator = GetComponent<VampiricUltimateActivator>();
        _damageableDetector = GetComponent<DamageableDetector>();

        _animator = GetComponentInChildren<PlayerAnimator>();

        gameObject.layer = LayerMask.NameToLayer(LayerMaskData.InString.Player);
    }

    private void OnEnable()
    {
        _inputReader.MovementKeyPressed += Move;
        _inputReader.MovementKeyReleased += StopMove;
        _inputReader.JumpKeyPressed += Jump;
        _inputReader.AttackKeyPressed += TryAttack;
        _inputReader.UltimateKeyPressed += UseUltimate;

        _health.IsOver += Die;

        _collector.KitPickedUp += UseKit;
    }

    private void OnDisable()
    {
        _inputReader.MovementKeyPressed -= Move;
        _inputReader.MovementKeyReleased -= StopMove;
        _inputReader.JumpKeyPressed -= Jump;
        _inputReader.AttackKeyPressed -= TryAttack;
        _inputReader.UltimateKeyPressed -= UseUltimate;
        
        _health.IsOver -= Die;

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
        if (_attacker.IsAttack) 
            return;

        if (_damageableDetector.TryDetect(out IDamageable detected, _attacker.AttackRange))
        {
            _attacker.Attack(detected);
            _attacker.StartDelayCoroutine();

            _animator.Attack();
        }
    }

    private void UseUltimate()
    {
        if (_mana.IsFull && _vampiricUltimateActivator.IsActive == false && _vampiricUltimateActivator.IsReload == false)
        {
            _vampiricUltimateActivator.Activate();
            _mana.Spend(_mana.CurrentValue); 
        }
    }

    private void UseKit(Kit kit)
    {
        if (_health == null)
            return;

        _health.Heal(kit.HeelAmount);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}