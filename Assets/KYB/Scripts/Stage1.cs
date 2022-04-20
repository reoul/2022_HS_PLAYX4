using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    public SpawnerManager _spawnerManager;

    public override void StageStart()
    {
        base.StageStart();
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.BGMChange("Straight Fuse - French Fuse", 1f);
        _spawnerManager = transform.GetChild(0).GetComponent<SpawnerManager>();
        _spawnerManager.SpawnerAwake();
    }

    public override void StageUpdate()
    {
        base.StageUpdate();
        _spawnerManager.SpawnerUpdate();
    }

    public override void StageEnd()
    {
        base.StageEnd();
        RemoveEnemy();
    }

    void RemoveEnemy()
    {
        var monsters = GetComponentsInChildren<TreeSpirit>(true);
        foreach (TreeSpirit monster in monsters)
        {
            monster.MoveSpeed = 0;
            var dissolveMat = monster.GetComponentInChildren<DissolveMat>();
            dissolveMat.StartDestroyDissolve();
            Destroy(monster.gameObject, 1.1f);
        }

        var unUsedEnemyQueue = FindObjectOfType<EnemySpawner>().unUsedEnemyQueue;
        int cnt = unUsedEnemyQueue.Count;

        for (int i = cnt; i > 0; i--)
        {
            Destroy(unUsedEnemyQueue.Dequeue(), 1.1f);
        }
    }
}