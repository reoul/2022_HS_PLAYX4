using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage : MonoBehaviour
{
    public List<GameObject> DissolveEnvironments;

    /// <summary>
    /// 주변 사물 소환
    /// </summary>
    public void StageSetUP()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                DissolveEnvironments[i * 7 + j].transform.position = new Vector3(10 + i * 3, 4, j * 3);
            }
        }
        SwapEnvironments();
        foreach (GameObject dissolve in DissolveEnvironments)
        {
            dissolve.GetComponent<KYB_Dissolve>().StartCreateDissolve();
        }
    }

    private void SwapEnvironments()
    {
        for (int i = 0; i < 100; i++)
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

    public void RemoveEnemy()
    {
    }

    public void RemoveStage()
    {
    }
}