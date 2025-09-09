using UnityEngine;

[RequireComponent (typeof(Health), typeof(Attacker), typeof(ItemCollector))]
public class Player : MonoBehaviour
{
    private ItemCollector _collector;

    private Health _health;
    private Attacker _attacker;

    private void Awake()
    {
        _collector = GetComponent<ItemCollector>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _collector.KitPickedUp += UseKit;
    }

    private void OnDisable()
    {
        _collector.KitPickedUp -= UseKit;
    }

    private void UseKit(Kit kit)
    {
        _health.Heel(kit.HeelAmount);
    }
}