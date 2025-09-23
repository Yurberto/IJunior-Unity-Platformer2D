using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(Attacker), typeof(UltimateEnemyDetector))]
public class VampiricUltimateActivator : MonoBehaviour
{
    [SerializeField, Range(0.0f, 50.0f)] private float _healthOverflowRate = 5.5f;

    [SerializeField, Range(0.0f, 10.0f)] private float _duration = 6.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float _reloadTime = 4.0f;

    [SerializeField] private Timer _timer;

    private UltimateEnemyDetector _detector;

    private Health _healthToHeal;
    private IDamageable _targetToDamage;

    private bool _ñanActivate => _timer.IsOn == false;

    private void Awake()
    {
        _detector = GetComponent<UltimateEnemyDetector>();
        _healthToHeal = GetComponent<Health>();
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
        if (_ñanActivate)
        {
            StartCoroutine(ActionCoroutine());
            _timer.Run(_duration);
        }
    }

    private IEnumerator ActionCoroutine()
    {
        _detector.StartDetect();

        var wait = new WaitForSeconds(Time.deltaTime);
        Debug.Log(_targetToDamage);

        while (_timer.IsOn)
        {
            if (_targetToDamage != null)
            {
                Debug.Log("Ïîøåë îòñîñ");
                _healthToHeal.Heal(_healthOverflowRate * Time.deltaTime);
                _targetToDamage.TakeDamage(_healthOverflowRate * Time.deltaTime);
            }

            yield return wait;
        }

        _timer.Run(_reloadTime);
        _detector.StopDetect();
    }

    private void SetTarget(Enemy target)
    {
        if (target.TryGetComponent(out Health health) == false)
            return;

        _targetToDamage = health;
    }
}
