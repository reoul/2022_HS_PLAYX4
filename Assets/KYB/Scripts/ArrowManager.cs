using System;
using UnityEngine;

public class ArrowManager : Singleton<ArrowManager>
{
    public void Shot(Vector3 positon, Vector3 direction)
    {
        RaycastHit[] hits = new RaycastHit[] { };
        int cnt = Physics.RaycastNonAlloc(positon, direction, hits, 1000);
        for (int i = 0; i < cnt; i++)
        {
            hits[i].collider.GetComponent<IHitable>()?.HitEvent();
        }
    }
} 