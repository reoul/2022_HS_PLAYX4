using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYB_EnemySpawner : MonoBehaviour
{
    private ObjectPooler _pooler;

    private enum EnemyType{ Spider = 1,Goblin, Lihano};

    [SerializeField]
    private EnemyType _enemyType;

    private void Awake()
    {
        _pooler = new ObjectPooler();
        _pooler.StartPooling(this.transform, ((int)_enemyType));
        //FindObjectOfType<SpawnerManager>()?.spawnerQueue.Enqueue(this);
    }

    public void Spawn()
    {
        GameObject spawnedObj;
        spawnedObj = _pooler.GetPooledObject();
        spawnedObj.transform.localPosition = Vector3.zero;
        spawnedObj.transform.LookAt(GameObject.Find("[CameraRig]").transform.position);
        spawnedObj.SetActive(true);
    }


}