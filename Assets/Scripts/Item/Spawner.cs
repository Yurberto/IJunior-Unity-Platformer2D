using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnpointsParent;
    [SerializeField] private Item _prefab;
    [SerializeField, Range(0, 10)] private float _spawnDelay = 6.0f;

    private SpawnProvider _spawnProvider;
    private Vector2[] _spawnpoints;
    private List<Vector2> _availableSpawnoints;

    private ObjectPool<Item> _pool;

    private void Awake()
    {
        _spawnpoints = new Vector2[_spawnpointsParent.childCount];
        _availableSpawnoints = new List<Vector2>(_spawnpointsParent.childCount);
        _spawnProvider = new SpawnProvider(_availableSpawnoints);

        _pool = new ObjectPool<Item>
            (
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (item) => GetAction(item),
            actionOnRelease: (item) => ReleaseAction(item),
            actionOnDestroy: (item) => Destroy(item.gameObject),
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

    private void GetAction(Item item)
    {
        Vector2 spawnpoint = _spawnProvider.GetSpawnPosition();

        item.gameObject.SetActive(true);
        item.transform.position = spawnpoint;

        _availableSpawnoints.Remove(spawnpoint);

        item.PickedUp += _pool.Release;
    }

    private void ReleaseAction(Item item)
    {
        item.gameObject.SetActive(false);
        _availableSpawnoints.Add(item.transform.position);

        item.PickedUp -= _pool.Release;

        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        var wait = new WaitForSeconds(_spawnDelay);
        yield return wait;

        _pool.Get();
    }
}
