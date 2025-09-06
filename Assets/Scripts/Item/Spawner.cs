using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected Transform _spawnpointsParent;
    [SerializeField] protected Gem _prefab;

    protected SpawnProvider _spawnProvider;
    protected Vector2[] _spawnpoints;
    protected List<Vector2> _availableSpawnoints;
    protected int _currentSpawnpoint = 0;

    protected ObjectPool<Gem> _pool;

    protected void Awake()
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
    }

    protected void GetAction(Gem gem)
    {
        Vector2 spawnpoint = _spawnProvider.GetSpawnPosition();

        gem.gameObject.SetActive(true);
        gem.transform.position = spawnpoint;

        _availableSpawnoints.Remove(spawnpoint);
    }

    protected void ReleaseAction(Gem gem)
    {
        gem.gameObject.SetActive(false);
        _availableSpawnoints.Add(gem.transform.position);
    }
}
