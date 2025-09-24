using System;
using UnityEngine;

public class Item : MonoBehaviour 
{
    public event Action<Item> PickedUp;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer(LayerMaskData.InString.Item);
    }

    public void PikeUp()
    {
        PickedUp?.Invoke(this);
    }
}
