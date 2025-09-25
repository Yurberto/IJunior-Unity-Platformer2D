using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class UltimateEnemyDetector : MonoBehaviour
{
    [SerializeField, Range(0.01f, 5.0f)] private float _ultimateRange = 3.0f;
    [SerializeField, Range(0.01f, 0.1f)] private float _offset = 0.01f;

    private Collider2D _characterCollider;
    private Vector2[] _rays;

    private void Awake()
    {
        _characterCollider = GetComponent<Collider2D>();
        _rays = new Vector2[]
            {
                new Vector2(-1, 0), new Vector2(1, 0)
            };
    }

    public bool TryDetect(out Enemy enemyToDetect)
    {
        Enemy nearestEnemy = null;
        bool isEnemyDetect = false;

        float nearestDistance = float.MaxValue;

        Vector2 rightPointX = new Vector2(_characterCollider.bounds.max.x + _offset, _characterCollider.bounds.center.y);
        Vector2 leftPointX = new Vector2(_characterCollider.bounds.min.x - _offset, _characterCollider.bounds.center.y);

        for(int i = 0; i < _rays.Length; i++)
        {
            Vector2 start = _rays[i].x > 0 ? rightPointX : leftPointX;
            RaycastHit2D hit = Physics2D.Raycast(start, _rays[i], _ultimateRange, LayerMaskData.Enemy);

            if (hit.collider != null && hit.collider.TryGetComponent(out Enemy enemy))
            {
                if (hit.distance < nearestDistance)
                {
                    nearestDistance = hit.distance;
                    nearestEnemy = enemy;
                }

                isEnemyDetect = true;
            }
        }

        if (isEnemyDetect)
            enemyToDetect = nearestEnemy;
        else
            enemyToDetect = null;
        
        return isEnemyDetect;
    }
}
