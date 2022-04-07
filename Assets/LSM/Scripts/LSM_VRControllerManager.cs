using System;
using System.Collections;
using UnityEngine;
using Valve.VR;

public class LSM_VRControllerManager : Singleton<LSM_VRControllerManager>
{
    public VRController LeftController { get; private set; }
    public VRController RightController { get; private set; }


    public AudioClip[] _chargeClip;
    AudioSource _chargeSource;

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
    /// 차징 속도
    /// </summary>
    private float _chargingSpeed = 360;

    /// <summary>
    /// 차징 딜레이 시간
    /// </summary>
    private const float _chargingDelay = 0.08f;

    /// <summary>
    /// 차징 할 때 두 컨트롤러 최대 거리
    /// </summary>
    private const float _chargingDistance = 0.3f;

    /// <summary>
    /// 차징이 100퍼센트 됬는지 체크
    /// </summary>
    public bool IsChargingFinish {get {return _chargingTime >= _maxCharging;}}

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

    public GameObject CameraObj;

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

            return BowController.CenterTransform.transform.position - ArrowController.CenterTransform.transform.position;
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
                return Vector3.Distance(BowController.CenterTransform.transform.position, ArrowController.CenterTransform.transform.position);
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

        if (Input.GetKey(KeyCode.D))
        {
            _chargingTime += 10 * Time.deltaTime;
            Debug.Log(_chargingTime);
        }

        if(Input.GetKeyUp(KeyCode.D))
        {
            _chargingTime -= 10 * Time.deltaTime;
        }

        ChargingSound();


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
                SetBowController(LeftController);
            }
        }
        else if (RightController.GetTriggerDown())
        {
            // 왼쪽 컨트롤러 트리거를 사용 안할때
            if (!(LeftController.GetTrigger() || LeftController.GetTriggerDown()))
            {
                SetBowController(RightController);
            }
        }

        if (BowController != null)
        {
            if (BowController.GetTriggerUp())
            {
                BowController.MeshON();
                BowManager.Instance.BowObj.SetActive(false);
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
            if (Distance > _chargingDistance)
            {
                return;
            }

            if (ArrowController.GetTriggerDown())
            {
                IsCharging = true;
                ArrowController.MeshOff();
                SetBow();
            }
        }
    }

    /// <summary>
    /// 화살을 발사하는 입력을 받았는지 체크
    /// </summary>
    private void CheckShot()
    {
        Debug.Log("asdasds");
        if ((BowController != null) && ArrowController.GetTriggerUp())
        {
        Debug.Log("123");
            if (_chargingTime >= _maxCharging)
            {
        Debug.Log("456");
                ArrowManager.Instance.Shot(RightController.transform.position, Direction);
                SoundManager.Instance.PlaySound("Rock 6-1", 1f);
            }
            ArrowController.MeshON();
            IsCharging = false;
            _chargingTime = 0;
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
                CreatePoint();
            }
            else if (controller.HandType == SteamVR_Input_Sources.RightHand)
            {
                RightController = controller;
            }
        }
    }

    private void CreatePoint()
    {
        Vector3 pos = new Vector3(0f, -0.072f, 0.044f);
        var leftGameObj = new GameObject("ControllerPoint");
        leftGameObj.transform.parent = LeftController.transform;
        leftGameObj.transform.localPosition = pos;
        var rightGameObj = new GameObject("ControllerPoint");
        rightGameObj.transform.parent = RightController.transform;
        rightGameObj.transform.localPosition = pos;
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

    private void SetBow()
    {
        GameObject bowOgj = FindObjectOfType<BowManager>().BowObj;
        bowOgj.transform.SetParent(BowController.transform);
        bowOgj.transform.position = Vector3.zero;
        bowOgj.transform.LookAt(bowOgj.transform.position - Camera.main.transform.position);
        bowOgj.SetActive(true);
    }

    /// <summary>
    /// 차징 진동
    /// </summary>
    private void StartChargingVibration()
    {
        Vibration(HandType.LeftRight, (int) _chargingTime);
    }

    private void ChargingSound()
    {
        if (IsCharging)
        {

            SoundManager.Instance.PlaySound("Buff 2-1", 0.8f);
            SoundManager.Instance.sfxPlayer.GetComponent<AudioSource>().pitch = Mathf.Lerp(1, 2, _chargingTime / _maxCharging);
        }

    }
    /// <summary>
    /// 차징 중일때 일정 시간마다 차징 게이지를 늘려줌
    /// </summary>
    private void ChargingTime()
    {
        Debug.Log(_chargingTime);
        //if(IsCharging)
        //{
        //    _chargingTime += _chargingSpeed * Time.deltaTime;
        //}
        //else
        //{
        //    _chargingTime -= _chargingSpeed * Time.deltaTime;
        //}

        if(Distance > _maxDistance)
        {
            _chargingTime += _chargingSpeed * Time.deltaTime;
        }
        else if (Distance >= Mathf.Lerp(0, _maxDistance, 0.5f))
        {
            _chargingTime += _chargingSpeed * Time.deltaTime;
            _chargingTime = Mathf.Clamp(_chargingTime, 0, Mathf.Lerp(0,_maxCharging,0.67f));
        }
        else
        {
            _chargingTime += _chargingSpeed * Time.deltaTime;
            _chargingTime = Mathf.Clamp(_chargingTime, 0, Mathf.Lerp(0,_maxCharging,0.34f));
        }

        



        // 두 컨트롤러 거리가 일정 이상 멀어졌을때(활 쏘는 동작을 했을 때)
        //if (Distance > Mathf.Lerp(0, _maxDistance, 0.7f))
        //{
        //    _chargingTime += _chargingSpeed * Time.deltaTime;
        //}
        //else
        //{
        //    _chargingTime -= _chargingSpeed * Time.deltaTime;
        //}
    }

    /// <summary>
    /// BowController 지정해주고 mesh를 꺼줌
    /// </summary>
    /// <param name="controller"></param>
    private void SetBowController(VRController controller)
    {
        BowController = controller;
        BowController.MeshOff();
        GameObject bowObj = BowManager.Instance.BowObj;
        bowObj.transform.parent = BowController.transform;
        bowObj.transform.transform.localPosition = Vector3.zero;
        bowObj.transform.localRotation = Quaternion.Euler(70, 0, 0);
        bowObj.SetActive(true);
    }

    /// <summary>
    /// 플레이어의 최대 팔 거리 업데이트
    /// </summary>
    private void UpdateMaxDistance()
    {
        //Debug.Log(Distance);
        if (Distance > _maxDistance)
        {
            _maxDistance = Distance;
            if (_maxDistance >= 0.6f)
            {
                _maxDistance = 0.6f;
            }
        }
    }
}