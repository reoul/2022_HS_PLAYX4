﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wendigo_Dead : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundFourth("Wendigo_Dead", 1f);
    }
}
