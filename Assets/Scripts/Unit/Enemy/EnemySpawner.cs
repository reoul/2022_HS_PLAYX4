using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnEnemy(1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnEnemy(2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnEnemy(3);
        }
    }

    private void SpawnEnemy(int type)
    {
        Enemy _stage1Enemy;
        switch (type)
        {
            case 1:
                _stage1Enemy = new EnemyBuilder("test").SetHealth(100).SetEnemyType(EnemyType.monter1).Build();
                break;
            case 2:
                _stage1Enemy = new EnemyBuilder("test").SetHealth(100).SetEnemyType(EnemyType.monter2).Build();
                break;
            case 3:
                _stage1Enemy = new EnemyBuilder("test").SetHealth(100).SetEnemyType(EnemyType.monter3).Build();
                break;
            default:
                _stage1Enemy = new EnemyBuilder("test").SetHealth(100).SetEnemyType(EnemyType.monter1).Build();
                break;
        }
        _stage1Enemy.gameObject.transform.parent = this.transform.parent;
        _stage1Enemy.gameObject.transform.localPosition = this.transform.localPosition;
        _stage1Enemy.gameObject.transform.LookAt(GameObject.Find("[CameraRig]").transform.position);
    }
}
