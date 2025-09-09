using UnityEngine;

[RequireComponent(typeof(ItemCollector), typeof(Health), typeof(Attacker))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private ItemCollector _collector;

    private Health _health;
    private Attacker _attacker;

    private void Awake()
    {
        _collector = GetComponent<ItemCollector>();
        _health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _inputReader.AttackKeyPressed += _attacker.TryAttack;
        _collector.KitPickedUp += UseKit;
    }

    private void OnDisable()
    {
        _inputReader.AttackKeyPressed -= _attacker.TryAttack;
        _collector.KitPickedUp -= UseKit;
    }

    private void UseKit(Kit kit)
    {
        _health.Heel(kit.HeelAmount);
    }
}