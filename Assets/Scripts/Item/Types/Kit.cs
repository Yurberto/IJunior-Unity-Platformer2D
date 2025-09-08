using System;
using UnityEngine;

public class Kit : Item 
{
    [SerializeField, Range(0, 100)] private float _heelAmount = 10;

    public float HeelAmount => _heelAmount;
}
