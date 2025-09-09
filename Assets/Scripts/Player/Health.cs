using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Range(50, 100)] private float _maxHealth = 100;
    [SerializeField, Range(0, 100)] private float _health = 100;

    private ItemCollector _collector;

    private void OnValidate()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

    private void Awake()
    {
        _collector = GetComponent<ItemCollector>();
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
        Heel(kit.HeelAmount);
    }

    private void Heel(float heelAmount)
    {
        if (heelAmount < 0)
            return;

        _health = Mathf.Min(_health + heelAmount, _maxHealth);
    }
}
