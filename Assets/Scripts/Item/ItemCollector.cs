using System;
using UnityEngine;

public class ItemCollector : MonoBehaviour 
{
    public event Action<Kit> KitPickedUp;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Item item))
        {
            item.PikeUp();

            if (item is Kit kit)
                KitPickedUp?.Invoke(kit);
        }
    }
}
