using System;
using System.Collections;
using UnityEngine;
using Valve.VR;

public class VRControllerManager : Singleton<VRControllerManager>
{
    public VRController LeftController { get; set; }
    public VRController RightController { get; set; }

    /// <summary>
    /// 차징하고 있는지
    /// </summary>
    public bool IsCharging { get; private set; }

    /// <summary>
    /// 차징된 시간
    /// </summary>
    private float _chargingTime = 0;

    /// <summary>
    /// 최대 차징 게이지
    /// </summary>
    private const float _maxCharging = 60;

    /// <summary>
    /// 차징 딜레이 주기 체크하는 변수
    /// </summary>
    private float _checkChargingTime = 0;

    /// <summary>
    /// 차징 딜레이 시간
    /// </summary>
    private const float _chargingDelay = 0.08f;

    /// <summary>
    /// 차징 할 때 두 컨트롤러 최대 거리
    /// </summary>
    private const float _chargingDistance = 0.2f;

    /// <summary>
    /// 플레이어의 두 컨트롤러 간의 최대 거리 (최대한 멀어질때마다 값 업데이트)
    /// </summary>
    private float _maxDistance;

    private readonly WaitForSeconds _delay008 = new WaitForSeconds(0.08f);

    /// <summary>
    /// 에임 고정 관련 스크립트
    /// </summary>
    private AutoAim _autoAim;
    
    /// <summary>
    /// 활을 들고 있는 컨트롤러
    /// </summary>
    public VRController BowController { get; set; }

    /// <summary>
    /// 활 시위를 당기고 있는 컨트롤러
    /// </summary>
    public VRController ArrowController
    {
        get { return BowController == LeftController ? RightController : LeftController; }
    }

    /// <summary>
    /// 화살 방향
    /// </summary>
    public Vector3 Direction
    {
        get
        {
            if (BowController == null)
            {
                return Vector3.zero;
            }

            // 에임 고정중이면
            if (_autoAim.IsAutoAnim)
            {
                return _autoAim.Target.transform.position - BowController.transform.position;
            }

            return BowController.transform.position - ArrowController.transform.position;
        }
    }

    /// <summary>
    /// 두 컨트롤러 간의 거리
    /// </summary>
    public float Distance
    {
        get
        {
            if (BowController != null)
            {
                return Vector3.Distance(BowController.transform.position, ArrowController.transform.position);
            }

            return 0;
        }
    }

    private void Awake()
    {
        FindController();
        IsCharging = false;
        _maxDistance = 0;
        _autoAim = FindObjectOfType<AutoAim>();
    }

    private void Update()
    {
        CheckBow();
        CheckCharging();
        UpdateMaxDistance();
        ChargingVibration();
        CheckShot();
    }

    /// <summary>
    /// BowController와 ArrowController를 지정해준다
    /// </summary>
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
        else if (RightController.GetTriggerDown())
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
                IsCharging = false;
                _chargingTime = 0;
            }
        }
    }

    /// <summary>
    /// 차징 시작했는지 확인
    /// </summary>
    private void CheckCharging()
    {
        if (IsCharging)
        {
            return;
        }

        if (BowController != null)
        {
            if (ArrowController.GetTriggerDown())
            {
                IsCharging = true;
            }
        }
    }

    /// <summary>
    /// 화살을 발사하는 입력을 받았는지 체크
    /// </summary>
    private void CheckShot()
    {
        if ((BowController != null) && ArrowController.GetTriggerUp())
        {
            // 두 컨트롤러가 멀 때
            if (Distance > _chargingDistance)
            {
                return;
            }
            
            if (_chargingTime >= _maxCharging)
            {
                ArrowManager.Instance.Shot(RightController.transform.position, Direction);
                IsCharging = false;
                _chargingTime = 0;
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
            if (controller.HandType == SteamVR_Input_Sources.LeftHand)
            {
                LeftController = controller;
            }
            else if (controller.HandType == SteamVR_Input_Sources.RightHand)
            {
                RightController = controller;
            }
        }
    }

    /// <summary>
    /// 진동 주기
    /// </summary>
    /// <param name="handType">컨트롤러 타입</param>
    /// <param name="frequency">진동 크기 (0~60)</param>
    public void Vibration(HandType handType, int frequency)
    {
        switch (handType)
        {
            case HandType.LeftRight:
                LeftController.Vibration(frequency);
                RightController.Vibration(frequency);
                break;
            case HandType.Left:
                LeftController.Vibration(frequency);
                break;
            case HandType.Right:
                RightController.Vibration(frequency);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(handType), handType, null);
        }
    }

    /// <summary>
    /// 차징 진동
    /// </summary>
    private void ChargingVibration()
    {
        if (IsCharging)
        {
            _checkChargingTime += Time.deltaTime;
            if (_checkChargingTime >= _chargingDelay)
            {
                _checkChargingTime -= _chargingDelay;
                ChargingTime();
                StartChargingVibration();
            }
        }
    }

    /// <summary>
    /// 차징 진동
    /// </summary>
    private void StartChargingVibration()
    {
        Vibration(HandType.LeftRight, (int) _chargingTime);
    }

    /// <summary>
    /// 차징 중일때 일정 시간마다 차징 게이지를 늘려줌
    /// </summary>
    private void ChargingTime()
    {
        // 두 컨트롤러 거리가 일정 이상 멀어졌을때(활 쏘는 동작을 했을 때)
        if (Distance > Mathf.Lerp(0, _maxDistance, 0.7f))
        {
            _chargingTime += 5f;
        }
    }

    /// <summary>
    /// 차징 시간에 따른 진동 주는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator VibrationCoroutine()
    {
        while (IsCharging)
        {
            Vibration(HandType.LeftRight, (int) _chargingTime);
            yield return _delay008;
        }

        yield return null;
    }

    /// <summary>
    /// 차징 시간 늘려주는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChargingTimeCoroutine()
    {
        _chargingTime = 0;
        while (IsCharging)
        {
            if (Distance > Mathf.Lerp(0, _maxDistance, 0.7f))
            {
                _chargingTime += 5f;
            }

            yield return _delay008;
        }

        yield return null;
    }

    /// <summary>
    /// 플레이어의 최대 팔 거리 업데이트
    /// </summary>
    private void UpdateMaxDistance()
    {
        if (Distance > _maxDistance)
        {
            _maxDistance = Distance;
        }
    }
}