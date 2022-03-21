using System;
using UnityEngine;
using Valve.VR;

public class ControllerManager : Singleton<ControllerManager>
{
    public SteamVR_Input_Sources handType;  //모두 사용, 왼손, 오른손
    public SteamVR_Action_Boolean grabAction;
    
    public SteamVR_Action_Vibration hapticAction;

    public void PlayVibration()
    {
        Pulse(1, 150, 75, SteamVR_Input_Sources.LeftHand);
        Pulse(1, 150, 75, SteamVR_Input_Sources.RightHand);
    }

    private void Update()
    {
        if (GetGrab())
        {
            PlayVibration();
        }
    }

    /// <summary>
    /// 진동
    /// </summary>
    /// <param name="duration">진동 지속 시간</param>
    /// <param name="frequency">진동 크기 0~320</param>
    /// <param name="amplitude">촉각 작용의 강도 0-1</param>
    /// <param name="source"></param>
    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0,duration, frequency, amplitude, source);
    }

    public bool GetGrab()
    {
        return grabAction.GetState(handType);
    }
}
