using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public int maxEnemyCount;
    public int spawnedEnemyCount;

    private float _spawnTimeDelay;

    public Queue<EnemySpawner> spawnerQueue = new Queue<EnemySpawner>();

    public void Spawn()
    {

    }
}
