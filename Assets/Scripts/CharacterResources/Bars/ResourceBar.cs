using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] protected Resources _resources;
    [SerializeField] protected FillZone _fillZone;

    protected void OnEnable()
    {
        _resources.ValueChanged += UpdateResourcesData;
    }

    protected void OnDisable()
    {
        _resources.ValueChanged -= UpdateResourcesData;
    }

    protected virtual void Start()
    {
        UpdateResourcesData();
    }

    protected virtual void UpdateResourcesData()
    {
        _fillZone.ApplyFill(_resources.CurrentValue / _resources.MaxValue);
    }
}
