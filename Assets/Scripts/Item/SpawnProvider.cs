using System.Collections.Generic;
using UnityEngine;

public class SpawnProvider
{
    private List<Vector2> _spawnpoints;

    public SpawnProvider(List<Vector2> spawnpoints)
    {
        _spawnpoints = spawnpoints; 
    }

    public Vector2 GetSpawnPosition()
    {
        int randomIndex = Random.Range(0, _spawnpoints.Count);

        return _spawnpoints[randomIndex];
    }
}
