using System.Collections;
using UnityEngine;

public class SmoothBar : ResourceBar 
{
    [SerializeField, Range(0.01f, 1.0f)] private float _changeSpeed = 1.0f;
    [SerializeField, Range(0.1f, 100.0f)] private float _timeFactor = 100.0f;

    private Coroutine _fillZoneChanger;

    protected override void Start()
    {
        _fillZone.ApplyFill(_resources.CurrentValue / _resources.MaxValue);
    }

    protected override void UpdateResourcesData()
    {
        if (_fillZoneChanger != null) 
            StopCoroutine(_fillZoneChanger);

        _fillZoneChanger = StartCoroutine(ChangeFillZoneCoroutine());
    }

    private IEnumerator ChangeFillZoneCoroutine()
    {
        float start = _fillZone.Value;
        float target = _resources.CurrentValue / _resources.MaxValue;
        float fillPercent = 0;

        var wait = new WaitForSeconds(Time.deltaTime);

        while (Mathf.Approximately(_fillZone.Value, target) == false)
        {
            _fillZone.ApplyFill(Mathf.Lerp(start, target, fillPercent));
            fillPercent += _changeSpeed / _timeFactor;
            yield return wait;
        }
    }
}
