using UnityEngine;

[RequireComponent(typeof(ItemCollector))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private ItemCollector _collector;

    private Health _health;
    private Attacker _attacker;
    private DamageableDetector _damageableDetector;

    private void Awake()
    {
        _collector = GetComponent<ItemCollector>();
        _health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();
        _damageableDetector = GetComponent<DamageableDetector>();
    }

    private void OnEnable()
    {
        _inputReader.AttackKeyPressed += TryAttack;
        _collector.KitPickedUp += UseKit;
    }

    private void OnDisable()
    {
        _inputReader.AttackKeyPressed -= TryAttack;
        _collector.KitPickedUp -= UseKit;
    }

    private void TryAttack()
    {
        if (_attacker == null || _attacker.IsAttack || _damageableDetector == null) 
            return;
        Debug.Log("Draw " + _attacker.IsAttack.ToString());

        if (_damageableDetector.TryDetect(out IDamageable detected, _attacker.AttackRange))
        {
            Debug.Log("Detected");
            _attacker.Attack(detected);
        }
    }

    private void UseKit(Kit kit)
    {
        if (_health == null)
            return;

        _health.Heel(kit.HeelAmount);
    }
}