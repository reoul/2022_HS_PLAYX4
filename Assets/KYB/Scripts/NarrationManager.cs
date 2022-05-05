using System;
using UnityEngine;

public class NarrationManager : Singleton<NarrationManager>
{
    public AudioSource AudioSource;
    public AudioClip[] clips;
    private int curIndex = -1;
    private void Update()
    {
        if (VRControllerManager.Instance.BowController.GetTrigger() &&
            VRControllerManager.Instance.ArrowController.GetTriggerDown())
        {
            PlayNextVoice();
        }
    }

    private void PlayVoice()
    {
        AudioSource.Play();
    }

    private void PlayNextVoice()
    {
        AudioSource.clip = clips[++curIndex];
        AudioSource.Play();
    }
}