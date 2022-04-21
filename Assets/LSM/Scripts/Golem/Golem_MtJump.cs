using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_MtJump : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("G_Hit_2", 1f);
    }
}
