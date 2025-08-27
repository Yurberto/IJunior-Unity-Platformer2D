using System.Collections;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField, Range(1, 100)] private float _spawnDelay = 1.0f;

    private Coroutine _coroutine;

    private void Awake()
    {
        _coroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        var wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            _spawner.Spawn();
            yield return wait;
        }
    }
}
