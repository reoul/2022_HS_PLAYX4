using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Stage : MonoBehaviour
{
    protected List<DissolveMatAll> DissolveEnvironments;

    public bool IsFinish { get; set; }
    
    
    /// <summary>
    /// 주변 사물 소환
    /// </summary>
    public void StageSetUP()
    {
        DissolveEnvironments.Swap(DissolveEnvironments.Count * 3);
        float k = 0;
        int cnt = 0;
        foreach (DissolveMatAll dissolve in DissolveEnvironments)
        {
            cnt++;
            if (cnt > 3)
            {
                k += 0.2f;
                cnt = 0;
            }
            dissolve.SetDissolveHeightMin();
            dissolve.StartCreateDissolve(k);
        }
    }

    public virtual void StageStart()
    {
        IsFinish = false;
        DissolveEnvironments = new List<DissolveMatAll>();
        foreach (var dissolveMatAll in GetComponentsInChildren<DissolveMatAll>())
        {
            DissolveEnvironments.Add(dissolveMatAll);
        }
    }

    public virtual void StageUpdate() { }

    public virtual void StageEnd()
    {
        IsFinish = true;
        RemoveEnvironment();
    }

    private void RemoveEnvironment()
    {
        foreach (DissolveMatAll dissolve in DissolveEnvironments)
        {
            dissolve.StartDestroyDissolve();
        }
    }
}