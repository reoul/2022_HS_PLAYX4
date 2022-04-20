using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroObj : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            this.gameObject.SetActive(false);
            SoundManager.Instance.PlaySoundSecond("Access Denied 6", 1f);
            StageManager.Instance.NextStage();
        }
    }
}
