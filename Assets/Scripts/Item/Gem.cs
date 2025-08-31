using System;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public event Action<Gem> PickedUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ItemCollector _))
            PickedUp?.Invoke(this);
    }
}
