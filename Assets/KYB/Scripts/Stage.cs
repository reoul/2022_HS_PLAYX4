using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Stage : MonoBehaviour
{
    public List<GameObject> DissolveEnvironments;

    public bool IsFinish { get; set; }
    
    
    /// <summary>
    /// 주변 사물 소환
    /// </summary>
    public void StageSetUP()
    {
        DissolveEnvironments.Swap(DissolveEnvironments.Count * 3);
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
            dissolve.GetComponent<DissolveMatAll>().SetDissolveHeightMin();
            dissolve.GetComponent<DissolveMatAll>().StartCreateDissolve(k);
        }
    }

    public virtual void StageStart()
    {
        IsFinish = false;
    }

    public virtual void StageUpdate() { }

    public virtual void StageEnd()
    {
        IsFinish = true;
        RemoveEnvironment();
    }

    private void RemoveEnvironment()
    {
        foreach (GameObject dissolve in DissolveEnvironments)
        {
            dissolve.GetComponent<DissolveMatAll>().StartDestroyDissolve();
        }
    }
}