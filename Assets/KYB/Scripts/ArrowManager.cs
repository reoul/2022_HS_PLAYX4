using System;
using UnityEngine;

public class ArrowManager : Singleton<ArrowManager>
{
    [SerializeField] private Transform ArrowTrans;
    
    public void Shot(Vector3 positon, Vector3 direction)
    {
        RaycastHit[] hits = new RaycastHit[] { };
        hits = Physics.RaycastAll(positon, direction, 1000);
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].collider.GetComponent<IHitable>()?.HitEvent();
        }
    }

    private void Update()
    {
        if (VRControllerManager.Instance.IsCharging)
        {
            ArrowTrans.position = VRControllerManager.Instance.ArrowController.transform.position;
        }
    }
}