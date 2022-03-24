using System;
using UnityEngine;
using Valve.VR;

public class VRControllerManager : Singleton<VRControllerManager>
{
    public VRController LeftController { get; set; }
    public VRController RightController { get; set; }

    private void Awake()
    {
        FindController();
    }

    // 반드시 처음에 컨트롤러를 찾아서 해당 변수에 적용시켜줘야한다.
    /// <summary>
    /// VR컨트롤러를 찾는다
    /// </summary>
    private void FindController()
    {
        foreach (var controller in FindObjectsOfType<VRController>())
        {
            switch (controller.HandType)
            {
                case SteamVR_Input_Sources.LeftHand:
                    LeftController = controller;
                    break;
                case SteamVR_Input_Sources.RightHand:
                    RightController = controller;
                    break;
            }

            Debug.Log(controller.name);
        }
    }

    /// <summary>
    /// 진동 주기
    /// </summary>
    /// <param name="handType">컨트롤러 타입</param>
    public void Vibration(SteamVR_Input_Sources handType)
    {
        switch (handType)
        {
            case SteamVR_Input_Sources.Any:
                LeftController.Vibration();
                RightController.Vibration();
                break;
            case SteamVR_Input_Sources.LeftHand:
                LeftController.Vibration();
                break;
            case SteamVR_Input_Sources.RightHand:
                RightController.Vibration();
                break;
        }
    }
}
