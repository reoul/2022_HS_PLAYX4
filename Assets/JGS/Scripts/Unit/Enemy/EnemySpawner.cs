using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Queue<GameObject> unUsedEnemyQueue;

    private enum EnemyType{ TreeSpirit = 1, Wendigo = 2};

    [SerializeField]
    private EnemyType _enemyType;

    public void Init()
    {
        unUsedEnemyQueue = new Queue<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            var enemy = new EnemyBuilder("Enemy").SetEnemyType(global::EnemyType.Wendigo).Build();
            enemy.gameObject.SetActive(false);
            enemy.transform.parent = StageManager.Instance._curStage.transform;
            enemy.GetComponent<Enemy>().score = DataManager.Instance.Data.WendigoScore;
            unUsedEnemyQueue.Enqueue(enemy.gameObject);
        }
    }

    public void Spawn(Vector3 pos)
    {
        var enemy = unUsedEnemyQueue.Dequeue().gameObject;
        enemy.transform.position = pos;
        int r = Random.Range(0, 4);
        float scale = 0.7f + r * 0.2f;
        enemy.transform.localScale = new Vector3(scale, scale,scale);
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().MoveSpeed = 3;
        enemy.GetComponent<Wendigo>().ChangeState(WendigoState.StateType.Spawn);
    }

    public void Delete(GameObject enemyObj)
    {
        enemyObj.SetActive(false);
        unUsedEnemyQueue.Enqueue(enemyObj);
        SpawnerManager.Instance.CurrentSpawnCount--;
    }

}
