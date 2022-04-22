using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroObj : MonoBehaviour, IHitable
{
    public void HitEvent()
    {
        SoundManager.Instance.PlaySoundSecond("Access Denied 6", 0.6f);
        Invoke("introObjHit", 0.5f);
    }

    private void introObjHit()
    {
        this.gameObject.SetActive(false);
        StageManager.Instance.NextStage();
    }
}
