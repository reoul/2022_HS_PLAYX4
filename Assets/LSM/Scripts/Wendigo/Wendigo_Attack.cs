using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wendigo_Attack : StateMachineBehaviour
{
    float attackTime = 0;
    bool attackCheck;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackCheck = true;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTime += Time.deltaTime;
        if (attackTime >= 0.9f && attackCheck == true)
        {
            SoundManager.Instance.PlaySoundThird("monster-groan-pain-SBA-300108043-preview", 1f);
            attackCheck = false;
        }
    }
}
