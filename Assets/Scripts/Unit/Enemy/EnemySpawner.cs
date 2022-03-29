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
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Enemy _stage1Enemy = new EnemyBuilder("test").SetHealth(100).SetEnemyType(EnemyType.monter1).Build();
        _stage1Enemy.gameObject.transform.parent = this.transform.parent;
        _stage1Enemy.gameObject.transform.localPosition = this.transform.localPosition;
        _stage1Enemy.gameObject.transform.LookAt(GameObject.Find("[CameraRig]").transform.position);
    }
}
