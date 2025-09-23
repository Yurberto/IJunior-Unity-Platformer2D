using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(Attacker), typeof(UltimateEnemyDetector))]
public class VampiricUltimateActivator : MonoBehaviour
{
    [SerializeField, Range(0.0f, 50.0f)] private float _healthOverflowRate = 5.5f;

    [SerializeField, Range(0.0f, 10.0f)] private float _duration = 6.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float _reloadTime = 4.0f;

    private UltimateEnemyDetector _detector;

    private Health _healthToHeal;
    private IDamageable _targetToDamage;

    private bool _isActive;
    private bool _isReload;

    private void Awake()
    {
        _detector = GetComponent<UltimateEnemyDetector>();
        _healthToHeal = GetComponent<Health>();

        _isActive = false;
        _isReload = false;
    }

    private void OnEnable()
    {
        _detector.NearestEnemyFound += SetTarget;
        _detector.EnemiesLost += () => _targetToDamage = null;
    }

    private void OnDisable()
    {
        _detector.NearestEnemyFound -= SetTarget;
        _detector.EnemiesLost -= () => _targetToDamage = null;
    }

    public void Activate()
    {
        if (_isActive == false && _isReload == false)
        {
            StartCoroutine(ActionCoroutine());
        }
    }

    private IEnumerator ActionCoroutine()
    {
        _isActive = true;
        _detector.StartDetect();

        float time = 0;

        var wait = new WaitForSeconds(Time.deltaTime);

        while (time < _duration)
        {
            if (_targetToDamage != null)
            {
                _healthToHeal.Heal(_healthOverflowRate * Time.deltaTime);
                _targetToDamage.TakeDamage(_healthOverflowRate * Time.deltaTime);
            }

            time += Time.deltaTime;
            yield return wait;
        }

        _isActive = false;
        _detector.StopDetect();

        Reload();
    }

    private IEnumerator Reload()
    {
        _isReload = true;
        yield return new WaitForSeconds(_reloadTime);
        _isReload = false;
    }

    private void SetTarget(Enemy target)
    {
        if (target.TryGetComponent(out Health health) == false)
            return;

        _targetToDamage = health;
    }
}
