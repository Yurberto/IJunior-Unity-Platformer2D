using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected void OnEnable()
    {
        Health.ValueChanged += UpdateHealthData;
    }

    protected void OnDisable()
    {
        Health.ValueChanged -= UpdateHealthData;
    }

    protected virtual void Start()
    {
        UpdateHealthData();
    }

    protected abstract void UpdateHealthData();
}
