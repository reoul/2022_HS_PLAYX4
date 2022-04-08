using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroObj : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            StageManager.Instance.NextStage();
            Destroy(this.gameObject);
        }
    }
}
