using System;
using UnityEngine;

public class BowManager : Singleton<BowManager>
{
    public LineRenderer _arrowTrajectoryLineRenderer;
    public LineRenderer _bowStringLineRenderer;
    public GameObject BowObj;

    private void Awake()
    {
        _arrowTrajectoryLineRenderer = GameObject.Find("ArrowTrajectoryLine").GetComponent<LineRenderer>();
        _bowStringLineRenderer = GameObject.Find("BowStringLine").GetComponent<LineRenderer>();

        _arrowTrajectoryLineRenderer.SetPosition(0, Vector3.zero);
        _arrowTrajectoryLineRenderer.SetPosition(1, Vector3.zero);
    }

    private void Update()
    {
        ShowArrowTrajectory();
        ShowBowString();
        UpdateRotate();
    }

    /// <summary>
    /// 화살의 궤적을 보여준다
    /// </summary>
    private void ShowArrowTrajectory()
    {
        // 차징하고 있지 않을 때
        if (!VRControllerManager.Instance.IsCharging)
        {
            _arrowTrajectoryLineRenderer.SetPosition(0, Vector3.zero);
            _arrowTrajectoryLineRenderer.SetPosition(1, Vector3.zero);
            return;
        }

        if (!VRControllerManager.Instance.IsChargingFinish)
        {
            _arrowTrajectoryLineRenderer.material.color = new Color(1, 0, 0, 0.5f);
        }
        else
        {
            _arrowTrajectoryLineRenderer.material.color = new Color(0, 1, 0, 0.5f);
        }

        _arrowTrajectoryLineRenderer.SetPosition(0,
            VRControllerManager.Instance.BowController.gameObject.transform.position);
        _arrowTrajectoryLineRenderer.SetPosition(1, VRControllerManager.Instance.Direction * 1000);
    }

    /// <summary>
    /// 활 시위를 보여준다
    /// </summary>
    private void ShowBowString()
    {
        // 차징하고 있지 않을 때
        if (!VRControllerManager.Instance.IsCharging)
        {
            _bowStringLineRenderer.SetPosition(0, Vector3.zero);
            _bowStringLineRenderer.SetPosition(1, Vector3.zero);
            _bowStringLineRenderer.SetPosition(2, Vector3.zero);
            return;
        }
        
        // todo : 포지션 수정하기
        Vector3 bowTopPos = Vector3.zero;
        Vector3 handPos = VRControllerManager.Instance.ArrowController.CenterTransform.transform.position;
        Vector3 bowBottomPos = Vector3.zero;
        _bowStringLineRenderer.SetPosition(0,bowTopPos);
        _bowStringLineRenderer.SetPosition(1,handPos);
        _bowStringLineRenderer.SetPosition(2,bowBottomPos);
    }

    private void UpdateRotate()
    {
        if (VRControllerManager.Instance.IsCharging)
        {
            BowObj.transform.position = VRControllerManager.Instance.BowController.CenterTransform.position;
            BowObj.transform.forward = VRControllerManager.Instance.Direction;
        }
        else
        {
            BowObj.transform.localRotation = Quaternion.Euler(70, 0, 0);
        }
    }
}