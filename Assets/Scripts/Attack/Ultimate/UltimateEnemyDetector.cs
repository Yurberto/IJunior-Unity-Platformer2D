using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class UltimateEnemyDetector : MonoBehaviour
{
    [SerializeField, Range(0.01f, 1.0f)] private float _detectDelay = 0.2f;

    [SerializeField, Range(0.01f, 5.0f)] private float _ultimateRange = 3.0f;
    [SerializeField, Range(0.01f, 0.1f)] private float _offset = 0.01f;

    private Coroutine _detectCoroutine;

    private Collider2D _characterCollider;
    private Vector2[] _rays;

    private float _nearestDistance;
    
    public event Action<Enemy> NearestEnemyFound;
    public event Action EnemiesLost;

    private void Awake()
    {
        _characterCollider = GetComponent<Collider2D>();
        _rays = new Vector2[]
            {
                Vector2.left, Vector2.right
            };

        _nearestDistance = float.MaxValue;
    }

    public void StartDetect()
    {
        if (_detectCoroutine != null)
            StopDetect();

        Debug.Log("StartDetect");
        _detectCoroutine = StartCoroutine(DetectCoroutine());
    }

    public void StopDetect()
    {
        StopCoroutine(_detectCoroutine);
        _detectCoroutine = null;
    }

    private IEnumerator DetectCoroutine()
    {
        var wait = new WaitForSeconds(_detectDelay);

        while (enabled)
        {
            Detect();
            yield return wait;
        }
    }

    private void Detect()
    {
        bool isEnemyDetect = false;
        Debug.Log("IsDetect");

        Vector2 rightPointX = new Vector2(_characterCollider.bounds.max.x + _offset, _characterCollider.bounds.center.y);
        Vector2 leftPointX = new Vector2(_characterCollider.bounds.min.x - _offset, _characterCollider.bounds.center.y);

        for (int i = 0; i < _rays.Length; i++)
        {
            Vector2 start = _rays[i].x > 0 ? rightPointX : leftPointX;
            RaycastHit2D hit = Physics2D.Raycast(start, _rays[i], _ultimateRange);
            Utils.DrawLine2D(start, start + _rays[i] * _ultimateRange, Color.red, 2);

            if (hit.collider == null || hit.collider.gameObject.TryGetComponent(out Enemy enemy) == false)
                continue;
            Debug.Log("DetectedEnemy");

            if (hit.distance < _nearestDistance)
            {
                Debug.Log("HasDetectedNew");
                _nearestDistance = hit.distance;
                NearestEnemyFound?.Invoke(enemy);
            }

            isEnemyDetect = true;
        }

        if (isEnemyDetect == false)
            EnemiesLost?.Invoke();
    }
}
