using System;
using UnityEngine;

public class BowManager : Singleton<BowManager>
{
    private LineRenderer _arrowTrajectoryLineRenderer;
    private LineRenderer _bowStringLineRenderer;
    private LineRenderer _bowStringLineRenderer2;
    public GameObject BowObj;
    private Transform _bowTopTransform;
    private Transform _bowBottomTransform;

    private void Awake()
    {
        _arrowTrajectoryLineRenderer = GameObject.Find("ArrowTrajectoryLine").GetComponent<LineRenderer>();
        _bowStringLineRenderer = GameObject.Find("BowStringLine").GetComponent<LineRenderer>();
        _bowStringLineRenderer2 = GameObject.Find("BowStringLine2").GetComponent<LineRenderer>();

        _arrowTrajectoryLineRenderer.SetPosition(0, Vector3.zero);
        _arrowTrajectoryLineRenderer.SetPosition(1, Vector3.zero);
        _bowTopTransform = BowObj.transform.GetChild(0);
        _bowBottomTransform = BowObj.transform.GetChild(1);
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
            _bowStringLineRenderer2.SetPosition(0, Vector3.zero);
            _bowStringLineRenderer2.SetPosition(1, Vector3.zero);
            return;
        }
        
        var arrowControllerPos = VRControllerManager.Instance.ArrowController.CenterTransform.transform.position;
        _bowStringLineRenderer.SetPosition(0, _bowTopTransform.position);
        _bowStringLineRenderer.SetPosition(1, arrowControllerPos);
        _bowStringLineRenderer2.SetPosition(0, arrowControllerPos);
        _bowStringLineRenderer2.SetPosition(1, _bowBottomTransform.position);
    }

    /// <summary>
    /// 활 각도 변경
    /// </summary>
    private void UpdateRotate()
    {
        /*if (VRControllerManager.Instance.IsCharging)
        {
            BowObj.transform.position = VRControllerManager.Instance.BowController.CenterTransform.position;
            BowObj.transform.forward = VRControllerManager.Instance.Direction;
        }
        else
        {
            //BowObj.transform.localRotation = Quaternion.Euler(70, 0, 0);
        }*/
    }
}