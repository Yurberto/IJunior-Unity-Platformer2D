using System.Collections;
using UnityEngine;

public class GemSpawnSystem : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField, Range(1, 100)] private float _spawnDelay = 1.0f;

    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _spawner.Initialize();
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        var wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            if (_spawner.CanSpawn)
                Spawn();

            yield return wait;
        }
    }

    private void Spawn()
    {
        var spawnedObject = _spawner.Spawn();

        if (spawnedObject.TryGetComponent(out Gem gem))
            gem.PickedUp += Release;
    }

    private void Release(Gem gem)
    {
        _spawner.Release(gem.gameObject);
        gem.PickedUp -= Release;
    }
}
