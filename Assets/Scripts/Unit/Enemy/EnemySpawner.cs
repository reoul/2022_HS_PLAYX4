using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float _spawnTimeDelay;
    private ObjectPooler _pooler;

    private enum EnemyType{ Spider = 1,Goblin, Lihano};

    [SerializeField]
    private EnemyType _enemyType;

    private void Start()
    {

        _spawnTimeDelay = Random.RandomRange(0, 10);
        _pooler = new ObjectPooler();
        _pooler.StartPooling(this.transform, ((int)_enemyType));
    }

    private void Update()
    {
        _spawnTimeDelay -= Time.deltaTime;
        if(_spawnTimeDelay < 0)
        {
            _spawnTimeDelay = Random.RandomRange(10, 15);
            GameObject spawnedObj;
            spawnedObj = _pooler.GetPooledObject();
            spawnedObj.transform.localPosition = Vector3.zero;
            spawnedObj.transform.LookAt(GameObject.Find("[CameraRig]").transform.position);
            spawnedObj.SetActive(true);
        }
        //{
        //    _spawnTimeDelay = Random.RandomRange(10, 15);
        //    ;
        //}
    }


}

//오브젝트 풀링

public class ObjectPooler : MonoBehaviour
{
    private GameObject SpawnEnemy(int type)
    {
        Enemy _stage1Enemy;
        switch (type)
        {
            case 1:
                _stage1Enemy = new EnemyBuilder("Spider").SetHealth(100).SetEnemyType(EnemyType.monter1).Build();
                break;
            case 2:
                _stage1Enemy = new EnemyBuilder("Goblin").SetHealth(100).SetEnemyType(EnemyType.monter2).Build();
                break;
            case 3:
                _stage1Enemy = new EnemyBuilder("Lihno").SetHealth(100).SetEnemyType(EnemyType.monter3).Build();
                break;
            default:
                _stage1Enemy = new EnemyBuilder("test").SetHealth(100).SetEnemyType(EnemyType.monter1).Build();
                break;
        }

        return _stage1Enemy.gameObject;
    }

    public int numberOfObject;
    private List<GameObject> gameObjects;
    private Transform _parant;
    private int _enemytype;

    public void StartPooling(Transform parant, int type)
    {
        numberOfObject = 3;
        gameObjects = new List<GameObject>();
        _parant = parant;
        for (int i = 0; i < numberOfObject; i++)
        {
            _enemytype = type;
            GameObject gameObject = SpawnEnemy(_enemytype).gameObject;
            gameObject.transform.parent = _parant;
            gameObject.SetActive(false);
            gameObjects.Add(gameObject);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (!gameObject.activeInHierarchy)
            {
                return gameObject;
            }
        }

        GameObject gameObj = SpawnEnemy(_enemytype).gameObject;
        gameObj.transform.parent = _parant;
        gameObj.SetActive(false);
        gameObjects.Add(gameObj);
        return gameObj;
    }
}