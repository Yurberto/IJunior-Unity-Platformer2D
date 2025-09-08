using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnpointsParent;
    [SerializeField] private Gem _prefab;
    [SerializeField, Range(0, 10)] private float _spawnDelay = 6.0f;

    private SpawnProvider _spawnProvider;
    private Vector2[] _spawnpoints;
    private List<Vector2> _availableSpawnoints;

    private ObjectPool<Gem> _pool;

    private void Awake()
    {
        _spawnpoints = new Vector2[_spawnpointsParent.childCount];
        _availableSpawnoints = new List<Vector2>(_spawnpointsParent.childCount);
        _spawnProvider = new SpawnProvider(_availableSpawnoints);

        _pool = new ObjectPool<Gem>
            (
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (gem) => GetAction(gem),
            actionOnRelease: (gem) => ReleaseAction(gem),
            actionOnDestroy: (gem) => Destroy(gem.gameObject),
            collectionCheck: true
            );
    }

    private void Start()
    {
        for (int i = 0; i < _spawnpointsParent.childCount; i++)
            _spawnpoints[i] = _spawnpointsParent.GetChild(i).position;

        foreach (Vector2 spawnpoint in _spawnpoints)
            _availableSpawnoints.Add(spawnpoint);

        while (_availableSpawnoints.Count > 0)
            _pool.Get();
    }

    private void GetAction(Gem gem)
    {
        Vector2 spawnpoint = _spawnProvider.GetSpawnPosition();

        gem.gameObject.SetActive(true);
        gem.transform.position = spawnpoint;

        _availableSpawnoints.Remove(spawnpoint);

        gem.PickedUp += _pool.Release;
    }

    private void ReleaseAction(Gem gem)
    {
        gem.gameObject.SetActive(false);
        _availableSpawnoints.Add(gem.transform.position);

        gem.PickedUp -= _pool.Release;

        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        var wait = new WaitForSeconds(_spawnDelay);
        yield return wait;

        _pool.Get();
    }
}
