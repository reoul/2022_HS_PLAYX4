﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_MtJump2 : StateMachineBehaviour
{
    float LandTime = 0;
    bool LandCheck = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("G_Hit", 5f);
        LandCheck = true;
        LandTime = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LandTime += Time.deltaTime;
        if (LandTime >= 1.8f && LandCheck == true)
        {
            SoundManager.Instance.PlaySoundSecond("G_Landing", 0.5f);
            LandCheck = false;
        }
    }
}