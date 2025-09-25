using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health), typeof(Attacker), typeof(UltimateEnemyDetector))]
public class VampiricUltimateAbillity : MonoBehaviour
{
    [SerializeField] private Image _ultimateSprite;
    [SerializeField, Range(0.0f, 50.0f)] private float _healthOverflowRate = 5.5f;

    [SerializeField, Range(0.0f, 10.0f)] private float _duration = 6.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float _reloadTime = 4.0f;

    private UltimateEnemyDetector _detector;

    public float Duration => _duration;
    public float ReloadTime => _reloadTime;

    private void Awake()
    {
        _detector = GetComponent<UltimateEnemyDetector>();
    }

    public bool TryStealHealth(out float stealedAmount)
    {
        if (_detector.TryDetect(out Enemy enemy) == false || enemy.TryGetComponent(out Health enemyHealth) == false)
        {
            stealedAmount = 0.0f;
            return false;
        }

        stealedAmount = _healthOverflowRate * Time.deltaTime;

        if (enemyHealth.CurrentValue - stealedAmount < 0)
        {
            stealedAmount -= (stealedAmount - enemyHealth.CurrentValue);
        }

        enemyHealth.TakeDamage(stealedAmount);
        return true;
    }
}
