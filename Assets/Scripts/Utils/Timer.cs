using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour 
{
    private bool _isOn;

    private Coroutine _currentCoroutine;

    public bool IsOn => _isOn;

    public void Run(float timeToWait)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        _currentCoroutine = StartCoroutine(WaitCoroutine(timeToWait));
    }

    private IEnumerator WaitCoroutine(float timeToWait)
    {
        _isOn = true;

        float time = 0;
        var wait = new WaitForSeconds(Time.deltaTime);

        while (time < timeToWait)
        {
            time += Time.deltaTime;
            yield return wait;
        }

        _isOn = false;
    }
}
