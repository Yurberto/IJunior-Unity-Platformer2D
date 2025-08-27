using System;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public event Action<Gem> PickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
            PickedUp?.Invoke(this);
    }
}
