using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour 
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnpointsParent;

    private ObjectPool<GameObject> _pool;

    private Transform[] _spawnpoints;
    private List<Vector2> _availableSpawnpoints;

    public bool CanSpawn => _availableSpawnpoints.Count > 0;

    private void Awake()
    {
        _spawnpoints = new Transform[_spawnpointsParent.childCount];
        _availableSpawnpoints = new List<Vector2>();

        _pool = new ObjectPool<GameObject>
            (
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (objectToGet) => GetAction(objectToGet),
            actionOnRelease: (objectToRelease) => ReleaseAction(objectToRelease),
            actionOnDestroy: (objectToDestroy) => Destroy(objectToDestroy.gameObject),
            collectionCheck: true
            );
    }

    public void Initialize()
    {
        for (int i = 0; i < _spawnpointsParent.childCount; i++)
            _spawnpoints[i] = _spawnpointsParent.GetChild(i);

        foreach (Transform spawnpoint in _spawnpoints)
            if (spawnpoint != null)
                _availableSpawnpoints.Add(spawnpoint.position);
    }

    public GameObject Spawn()
    {
        return _pool.Get();
    }

    public void Release(GameObject objectToRelease)
    {
        _pool.Release(objectToRelease);
    }

    private void GetAction(GameObject objectToGet)
    {
        objectToGet.SetActive(true);
        objectToGet.transform.position = GetAvailablePosition();
    }

    private void ReleaseAction(GameObject objectToRelease)
    {
        objectToRelease.SetActive(false);
        _availableSpawnpoints.Add(objectToRelease.transform.position);
    }

    private Vector2 GetAvailablePosition()
    {
        int randomIndex = Random.Range(0, _availableSpawnpoints.Count - 1);
        Vector2 position = _availableSpawnpoints[randomIndex];
        _availableSpawnpoints.RemoveAt(randomIndex);

        return position;
    }
}
