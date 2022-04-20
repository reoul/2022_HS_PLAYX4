using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSound : MonoBehaviour
{
    
    public void GolemRoar()
    {
        SoundManager.Instance.PlaySoundThird("G_Attack_1", 1f);
    }
}
