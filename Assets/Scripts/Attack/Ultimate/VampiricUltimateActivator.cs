using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health), typeof(Attacker), typeof(UltimateEnemyDetector))]
public class VampiricUltimateActivator : MonoBehaviour
{
    [SerializeField] private Image _ultimateSprite;
    [SerializeField, Range(0.0f, 50.0f)] private float _healthOverflowRate = 5.5f;

    [SerializeField, Range(0.0f, 10.0f)] private float _duration = 6.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float _reloadTime = 4.0f;

    private UltimateEnemyDetector _detector;

    private Coroutine _action;

    private Health _healthToHeal;
    private Health _healthToDamage;

    private bool _isActive;
    private bool _isReload;

    public bool IsActive => _isActive;
    public bool IsReload => _isReload;

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
        _detector.EnemiesLost += () => _healthToDamage = null;
    }

    private void OnDisable()
    {
        _detector.NearestEnemyFound -= SetTarget;
        _detector.EnemiesLost -= () => _healthToDamage = null;
    }

    public void Activate()
    {
        if (_action != null)
        {
            StopCoroutine(_action);
            _action = null;
        }

        _action = StartCoroutine(ActionCoroutine());
    }

    private IEnumerator ActionCoroutine()
    {
        _isActive = true;
        _detector.StartDetect();
        _ultimateSprite.gameObject.SetActive(_isActive);

        float time = 0;

        while (time < _duration)
        {
            if (_healthToDamage != null && _healthToDamage.CurrentValue > 0)
            {
                _healthToHeal.Heal(_healthOverflowRate * Time.deltaTime);
                _healthToDamage.TakeDamage(_healthOverflowRate * Time.deltaTime);
            }

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _isActive = false;
        _detector.StopDetect();
        _ultimateSprite.gameObject.SetActive(_isActive);

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

        _healthToDamage = health;
    }
}
