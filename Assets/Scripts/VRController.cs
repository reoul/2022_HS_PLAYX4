using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// VR 컨트롤러 핸드 타입
/// </summary>
public enum HandType
{
    Left,
    Right,
    LeftRight
}

public class VRController : MonoBehaviour
{
    public SteamVR_Input_Sources HandType;  //모두 사용, 왼손, 오른손
    public SteamVR_Action_Boolean GrabAction;
    
    public SteamVR_Action_Vibration HapticAction;

    public void Vibration()
    {
        Pulse(1, 150, 75, HandType);
    }

    private void Update()
    {
        if (GetGrab())
        {
            Vibration();
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
        HapticAction.Execute(0,duration, frequency, amplitude, source);
    }

    /// <summary>
    /// 트리거 버튼이 작동 중 상태
    /// </summary>
    /// <returns>트리거 상태</returns>
    public bool GetGrab()
    {
        return GrabAction.GetState(HandType);
    }
}
