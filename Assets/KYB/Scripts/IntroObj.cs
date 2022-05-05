﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroObj : MonoBehaviour, IHitable
{
    public void HitEvent()
    {
        this.gameObject.SetActive(false);
        StageManager.Instance.NextStage();
        NarrationManager.Instance.IsCheckFlag = false;
        SoundManager.Instance.PlaySoundSecond("Access Denied 3", 0.2f);
    }
}
