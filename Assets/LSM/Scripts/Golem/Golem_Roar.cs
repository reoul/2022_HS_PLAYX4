using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Roar : StateMachineBehaviour
{
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("monster-whirr-SBA-300063842-preview", 1f);
    }
   
}
