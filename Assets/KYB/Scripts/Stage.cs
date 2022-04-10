using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Stage : MonoBehaviour
{
    public List<GameObject> DissolveEnvironments;

    /// <summary>
    /// 주변 사물 소환
    /// </summary>
    public void StageSetUP()
    {
        SwapEnvironments();
        float k = 0;
        int cnt = 0;
        foreach (GameObject dissolve in DissolveEnvironments)
        {
            cnt++;
            if (cnt > 3)
            {
                k += 0.2f;
                cnt = 0;
            }
            dissolve.GetComponent<DissolveMat>().StartCreateDissolve(k);
        }
    }

    private void SwapEnvironments()
    {
        for (int i = 0; i < DissolveEnvironments.Count * 3; i++)
        {
            int src = Random.Range(0, DissolveEnvironments.Count);
            int dest;
            do
            {
                dest = Random.Range(0, DissolveEnvironments.Count);
            } while (src == dest);
            (DissolveEnvironments[src], DissolveEnvironments[dest]) =
                (DissolveEnvironments[dest], DissolveEnvironments[src]);
        }
    }

    /// <summary>
    /// 몬스터 소환 시작
    /// </summary>
    public void StartEnemySpawn()
    {
    }

    public abstract void StageStart();

    public abstract void StageUpdate();

    public virtual void RemoveEnemy()
    {
        var monsters = FindObjectsOfType<StrategySpider>();
        foreach (StrategySpider monster in monsters)
        {
            monster.GetComponentInChildren<DissolveMat>().StartDestroyDissolve();
        }
    }

    public void RemoveStage()
    {
        RemoveEnemy();
        foreach (GameObject dissolve in DissolveEnvironments)
        {
            dissolve.GetComponent<DissolveMat>().StartDestroyDissolve();
        }
    }
}