using System;
using UnityEngine;

public class Item : MonoBehaviour 
{
    public event Action<Item> PickedUp;

    public void PikeUp()
    {
        PickedUp?.Invoke(this);
    }
}
