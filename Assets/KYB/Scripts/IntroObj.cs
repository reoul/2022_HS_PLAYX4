using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroObj : MonoBehaviour
{
    private void introObjHit()
    {        
        this.gameObject.SetActive(false);
        StageManager.Instance.NextStage();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            SoundManager.Instance.PlaySoundSecond("Access Denied 6", 0.8f);
            Invoke("introObjHit", 0.5f);
        }
    }
}
