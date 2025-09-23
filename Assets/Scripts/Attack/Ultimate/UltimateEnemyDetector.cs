using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class UltimateEnemyDetector : MonoBehaviour
{
    [SerializeField, Range(0.01f, 5.0f)] private float _ultimateRange = 3.0f;
    [SerializeField, Range(0.01f, 0.1f)] private float _offset = 0.01f;

    private Collider2D _characterCollider;
    private Vector2[] _rays;

    private float _nearestDistance = float.MaxValue;
    
    public event Action<Enemy> FoundNearestEnemy;
    public event Action LostEnemies;

    private void Awake()
    {
        _characterCollider = GetComponent<Collider2D>();
        _rays = new Vector2[]
            {
                Vector2.left, Vector2.right
            };
    }

    public void Detect()
    {
        bool isEnemyDetect = false;

        Vector2 rightPointX = new Vector2(_characterCollider.bounds.max.x + _offset, _characterCollider.bounds.center.y);
        Vector2 leftPointX = new Vector2(_characterCollider.bounds.min.x - _offset, _characterCollider.bounds.center.y);

        for (int i = 0; i < _rays.Length; i++)
        {
            Vector2 start = _rays[i].x > 0 ? rightPointX : leftPointX;
            RaycastHit2D hit = Physics2D.Raycast(start, _rays[i], _ultimateRange);

            if (hit.collider == null || hit.collider.gameObject.TryGetComponent(out Enemy enemy) == false)
                continue;

            if (hit.distance < _nearestDistance)
            {
                _nearestDistance = hit.distance;
                FoundNearestEnemy?.Invoke(enemy);
            }

            isEnemyDetect = true;
        }

        if (isEnemyDetect == false)
            LostEnemies?.Invoke();
    }
}
