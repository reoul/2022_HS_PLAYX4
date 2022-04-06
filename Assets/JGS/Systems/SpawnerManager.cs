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
            Debug.Log("Shuffle!");
        }
    }

    private void Start()
    {
        _spawnDelay = 0.5f;
        _currentTime = 0;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _spawnDelay)
        {
            _currentTime = 0;
            Spawn();
        }
    }
}
