using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYB_WeakPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arrow")
        {
            ScoreSystem.Score += 100;
            Debug.Log("adsasdasdada");
            this.transform.GetComponentInParent<MonsterT>().ChangeWeakPoint();
        }
    }
}
