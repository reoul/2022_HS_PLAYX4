using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroObj : MonoBehaviour, IHitable
{
    public void HitEvent()
    {
        Invoke("introObjHit", 0);
        SoundManager.Instance.PlaySoundSecond("Access Denied 3", 0.2f);
    }

    private void introObjHit()
    {
        this.gameObject.SetActive(false);
        StageManager.Instance.NextStage();
    }
}
