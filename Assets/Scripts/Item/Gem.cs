using System;
using UnityEngine;

public class Gem : MonoBehaviour 
{
    public event Action<Gem> PickedUp;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out ItemCollector _))
            PickedUp?.Invoke(this);
    }
}
