using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _spawnpointsParent;

    private Transform[] _spawnpoints;
    private List<Vector2> _availableSpawnpoints;
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _spawnpoints = new Transform[_spawnpointsParent.childCount];
        _availableSpawnpoints = new List<Vector2>();
        _pool = new ObjectPool<Enemy>
            (
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (enemy) => GetAction(enemy),
            actionOnRelease: (enemy) => ReleaseAction(enemy),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true
            );
    }

    private void Start()
    {
        for (int i = 0; i < _spawnpointsParent.childCount; i++)
            _spawnpoints[i] = _spawnpointsParent.GetChild(i);

        foreach (Transform spawnpoint in _spawnpoints)
            _availableSpawnpoints.Add(spawnpoint.position);
    }

    public void Spawn(Enemy enemy)
    {
        _pool.Get();
    }

    public void Release(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private void GetAction(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = GetAvailablePosition();
    }

    private void ReleaseAction(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        _availableSpawnpoints.Add(enemy.transform.position);
    }

    private Vector2 GetAvailablePosition()
    {
        int randomIndex = Random.Range(0, _availableSpawnpoints.Count - 1);
        Vector2 position = _availableSpawnpoints[randomIndex];
        _availableSpawnpoints.RemoveAt(randomIndex);

        return position;
    }
}
