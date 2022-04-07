using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    public Queue<EnemySpawner> spawnerQueue = new Queue<EnemySpawner>();
    private List<EnemySpawner> usedSpawnerList = new List<EnemySpawner>();
    private EnemySpawner currentSpawner;
    
    private float _spawnDelay;
    private float _currentTime;

    [SerializeField]
    private int _maxSpawnCount = 5;

    public int _currentSpawnCount;

    
    public void Spawn()
    {
        currentSpawner = spawnerQueue.Dequeue();
        currentSpawner.Spawn();
        usedSpawnerList.Add(currentSpawner);
        if (spawnerQueue.Count <= 0)
        {
            usedSpawnerList = Utility.ShuffleList(usedSpawnerList);
            foreach (var obj in usedSpawnerList)
            {
                spawnerQueue.Enqueue(obj);
            }
            usedSpawnerList.Clear();
        }
    }

    private void Start()
    {
        _currentSpawnCount = 0;
        _spawnDelay = 0.5f;
        _currentTime = 0;
    }

    public void SpawnUpdate()
    {
        _currentTime += Time.deltaTime;
        if ((_currentTime > _spawnDelay) && (_currentSpawnCount < _maxSpawnCount))
        {
            _currentTime = 0;
            _currentSpawnCount++;
            Spawn();
        }
    }
}
