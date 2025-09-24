using System.Collections;
using UnityEngine;

public class Mana : Resources
{
    [SerializeField, Range(0.0f, 100.0f)] private float _increasePerSecond = 10;

    private Coroutine _passiveRegen;

    private void Start()
    {
        _passiveRegen = StartCoroutine(PassiveRegen());
    }

    public void Restore(float manaAmount)
    {
        Increase(manaAmount);
    }

    public void Spend(float manaAmount)
    {
        Decrease(manaAmount);
    }

    private IEnumerator PassiveRegen()
    {
        while (enabled)
        {
            Restore(Mathf.MoveTowards(_currentValue, _maxValue, _increasePerSecond * Time.deltaTime) - _currentValue);
            yield return new WaitForEndOfFrame();
        }
    }
}
