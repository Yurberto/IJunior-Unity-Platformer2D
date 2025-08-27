using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Gem _prefab;
    [SerializeField] private List<Transform> _spawnpoints;

    private List<Vector2> _availableSpawnpoints;
    private ObjectPool<Gem> _pool;

    private void Awake()
    {
        _availableSpawnpoints = new List<Vector2>();
        _pool = new ObjectPool<Gem>
            (
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (gem) => GetAction(gem),
            actionOnRelease: (gem) => ReleaseAction(gem),
            collectionCheck: true
            );
    }

    private void Start()
    {
        foreach (Transform point in _spawnpoints)
            _availableSpawnpoints.Add(point.position);
    }

    public void Spawn()
    {
        _pool.Get();
    }

    private void Release(Gem gem)
    {
        _pool.Release(gem);
    }

    private void GetAction(Gem gem)
    {
        gem.gameObject.SetActive(true);
        gem.transform.position = GetAvailablePosition();
        gem.PickedUp += Release;
    }

    private void ReleaseAction(Gem gem)
    {
        gem.gameObject.SetActive(false);
        _availableSpawnpoints.Add(gem.transform.position);
        gem.PickedUp -= Release;
    }

    private Vector2 GetAvailablePosition()
    {
        int randomIndex = Random.Range(0, _availableSpawnpoints.Count - 1);
        Vector2 position = _availableSpawnpoints[randomIndex];
        _availableSpawnpoints.RemoveAt(randomIndex);

        return position;
    }
}
