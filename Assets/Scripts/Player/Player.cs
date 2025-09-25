using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ItemCollector), typeof(DamageableDetector), typeof(Mover))]
[RequireComponent(typeof(Jumper), typeof(Health), typeof(Attacker))]
[RequireComponent (typeof(VampiricUltimateAbillity), typeof(Mana))]
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
    private VampiricUltimateAbillity _vampiricUltimateAbility;

    private DamageableDetector _damageableDetector;

    private bool _canUseUltimate = true;

    public event Action UltimateUsed;
    public event Action UltimateReload;

    private void Awake()
    {
        _collector = GetComponent<ItemCollector>();

        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();

        _health = GetComponent<Health>();
        _mana = GetComponent<Mana>();

        _attacker = GetComponent<Attacker>();
        _vampiricUltimateAbility = GetComponent<VampiricUltimateAbillity>();
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
        if (_mana.IsFull && _canUseUltimate)
        {
            StartCoroutine(VampiricUltimateCoroutine());
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

    private IEnumerator VampiricUltimateCoroutine()
    {
        UltimateUsed?.Invoke();
        _canUseUltimate = false;

        float timer = 0;

        while (timer < _vampiricUltimateAbility.Duration)
        {
            if (_vampiricUltimateAbility.TryStealHealth(out float stealedAmount))
                _health.Heal(stealedAmount);

            timer += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        UltimateReload?.Invoke();
        yield return new WaitForSeconds(_vampiricUltimateAbility.ReloadTime);
        _canUseUltimate = true;
    }
}