using System;
using UnityEngine;
using Valve.VR;

public class VRControllerManager : Singleton<VRControllerManager>
{
    public VRController LeftController { get; set; }
    public VRController RightController { get; set; }

    private bool _isCharging;
    
    
    /// <summary>
    /// 활을 들고 있는 컨트롤러
    /// </summary>
    public VRController BowController { get; set; }
    
    public VRController ArrowController
    {
        get { return BowController == LeftController ? RightController : LeftController;  }
    }

    public Vector3 direction
    {
        get
        {
            if (BowController == null)
            {
                return Vector3.zero;
            }
            return BowController.transform.position - ArrowController.transform.position;
        }
    }

    public float distance
    {
        get
        {
            if (BowController == null)
            {
                return 0;
            }
            return Vector3.Distance(BowController.transform.position, ArrowController.transform.position);
        }
    }

    private void Awake()
    {
        FindController();
    }

    private void Update()
    {
        CheckBow();
        CheckCharging();
        
        if ((BowController != null) && ArrowController.GetTriggerUp())
        {
            ArrowManager.Instance.Shot(RightController.transform.position, direction);
        }
        
    }

    private void CheckBow()
    {
        if (LeftController.GetTriggerDown())
        {
            // 오른쪽 컨트롤러 트리거를 사용 안할때
            if (!(RightController.GetTrigger() || RightController.GetTriggerDown()))
            {
                BowController = LeftController;
            }
        }
        else if(RightController.GetTriggerDown())
        {
            // 왼쪽 컨트롤러 트리거를 사용 안할때
            if (!(LeftController.GetTrigger() || LeftController.GetTriggerDown()))
            {
                BowController = RightController;
            }
        }

        if (BowController != null)
        {
            if (BowController.GetTriggerUp())
            {
                BowController = null;
            }
        }
    }

    private void CheckCharging()
    {
        if (_isCharging)
        {
            return;
        }
        
        if (BowController != null)
        {
            if (ArrowController.GetTriggerDown())
            {
                _isCharging = true;
            }
        }
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
