using System;
using UnityEngine;

public class Gem : MonoBehaviour 
{
    public event Action<Gem> PickedUp;

    public void PikeUp()
    {
        PickedUp?.Invoke(this);
    }
}
