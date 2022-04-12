using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    public Queue<Transform> usedSpawnTransQueue = new Queue<Transform>();
    public Queue<Transform> unusedSpawnTransQueue = new Queue<Transform>();
    
    private float _spawnDelay;
    private float _currentTime;

    [SerializeField]
    private int _maxSpawnCount = 5;

    public int _currentSpawnCount;

    
    public void Spawn()
    {
        if(unusedSpawnTransQueue.Count <= 0)
        {
            List<Transform> shuffleList = new List<Transform>();
            for (int i = 0; i < usedSpawnTransQueue.Count;)
            {
                shuffleList.Add(usedSpawnTransQueue.Dequeue());
            }
            Utility.ShuffleList(shuffleList);
            foreach (Transform spawner in shuffleList)
            {
                unusedSpawnTransQueue.Enqueue(spawner);
            }
        }

        Transform unUsedTrans = unusedSpawnTransQueue.Dequeue();
        FindObjectOfType<EnemySpawner>().Spawn(unUsedTrans.position);
        usedSpawnTransQueue.Enqueue(unUsedTrans);
    }

    private void Start()
    {
        _currentSpawnCount = 0;
        _spawnDelay = 0.5f;
        _currentTime = 0;
        var spawnTransforms = GetComponentsInChildren<Transform>();
        for (int i = 1; i < spawnTransforms.Length; i++)
        {
            unusedSpawnTransQueue.Enqueue(spawnTransforms[i]);
        }
    }

    public void SpawnUpdate()
    {
        _currentTime += Time.deltaTime;
        if (StageManager.Instance._curStage.IsFinish)
        {
            return;
        }
        if ((_currentTime > _spawnDelay) && (_currentSpawnCount < _maxSpawnCount))
        {
            _currentTime -= _spawnDelay;
            _currentSpawnCount++;
            Spawn();
        }
    }
}
