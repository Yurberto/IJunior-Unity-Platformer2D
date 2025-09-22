using System.Collections;
using UnityEngine;

public class SmoothHealthBar : MonoBehaviour 
{
    [SerializeField] protected Health Health;
    [SerializeField] protected FillZone FillZone;

    [SerializeField, Range(0.01f, 1.0f)] private float _changeSpeed = 1.0f;
    [SerializeField, Range(0.1f, 100.0f)] private float _timeFactor = 100.0f;

    private Coroutine _healthChanger;

    private void OnEnable()
    {
        Health.ValueChanged += UpdateHealthData;
    }

    private void OnDisable()
    {
        Health.ValueChanged -= UpdateHealthData;
    }

    private void Start()
    {
        UpdateHealthData();
    }

    private void UpdateHealthData()
    {
        if (_healthChanger != null) 
            StopCoroutine(_healthChanger);

        _healthChanger = StartCoroutine(ChangeHealthCoroutine());
    }

    private IEnumerator ChangeHealthCoroutine()
    {
        float start = FillZone.Value;
        float target = Health.CurrentValue / Health.MaxValue;
        float time = 0;

        var wait = new WaitForSeconds(Time.deltaTime);

        while (Mathf.Approximately(FillZone.Value, target) == false)
        {
            FillZone.ApplyFill(Mathf.Lerp(start, target, time));
            time += _changeSpeed / _timeFactor;
            yield return wait;
        }
    }
}
