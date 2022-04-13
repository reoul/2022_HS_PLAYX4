using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Queue<GameObject> unUsedEnemyQueue;

    private enum EnemyType{ TreeSpirit = 1};

    [SerializeField]
    private EnemyType _enemyType;

    private void Start()
    {
        unUsedEnemyQueue = new Queue<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            var enemy = new EnemyBuilder("TreeSpirit").Build();
            enemy.gameObject.SetActive(false);
            unUsedEnemyQueue.Enqueue(enemy.gameObject);
        }
    }

    public void Spawn(Vector3 pos)
    {
        var enemy = unUsedEnemyQueue.Dequeue().gameObject;
        enemy.transform.position = pos;
        enemy.SetActive(true);
        enemy.GetComponent<TreeSpirit>().MoveSpeed = 5;
        enemy.GetComponent<TreeSpirit>().ChangeState(TreeSpiritState.StateType.Spawn);
    }

    public void Delete(GameObject enemyObj)
    {
        enemyObj.SetActive(false);
        unUsedEnemyQueue.Enqueue(enemyObj);
        SpawnerManager.Instance.CurrentSpawnCount--;
    }

}
