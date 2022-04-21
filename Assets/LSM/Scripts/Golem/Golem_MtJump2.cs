using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_MtJump2 : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySoundThird("G_Hit", 5f);
    }
}
