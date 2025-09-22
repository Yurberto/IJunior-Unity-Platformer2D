using System;
using UnityEngine;

[RequireComponent(typeof(ItemCollector))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private ItemCollector _collector;

    private Mover _mover;
    private Jumper _jumper;

    private Health _vitality;
    private Attacker _attacker;

    private DamageableDetector _damageableDetector;

    public event Action Attacked;

    private void Awake()
    {
        _collector = GetComponent<ItemCollector>();

        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();

        _vitality = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();

        _damageableDetector = GetComponent<DamageableDetector>();
    }

    private void OnEnable()
    {
        _inputReader.MovementKeyPressed += _mover.Move;
        _inputReader.MovementKeyReleased += _mover.StopMove;
        _inputReader.JumpKeyPressed += _jumper.Jump;
        _inputReader.AttackKeyPressed += TryAttack;
        _collector.KitPickedUp += UseKit;
    }

    private void OnDisable()
    {
        _inputReader.MovementKeyPressed -= _mover.Move;
        _inputReader.MovementKeyReleased -= _mover.StopMove;
        _inputReader.JumpKeyPressed -= _jumper.Jump;
        _inputReader.AttackKeyPressed -= TryAttack;
        _collector.KitPickedUp -= UseKit;
    }

    private void TryAttack()
    {
        if (_attacker == null || _attacker.IsAttack || _damageableDetector == null) 
            return;

        if (_damageableDetector.TryDetect(out IDamageable detected, _attacker.AttackRange))
            _attacker.Attack(detected);

        _attacker.StartDelayCoroutine();

        Attacked?.Invoke();
    }

    private void UseKit(Kit kit)
    {
        if (_vitality == null)
            return;

        _vitality.Heal(kit.HeelAmount);
    }
}