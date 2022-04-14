﻿using System;
using UnityEngine;

public class BowManager : Singleton<BowManager>
{
    private LineRenderer _arrowLineRenderer;
    private LineRenderer _arrowTrajectoryLineRenderer;
    private LineRenderer _bowStringLineRenderer;
    private LineRenderer _bowStringLineRenderer2;
    public GameObject BowObj;
    private Transform _bowTopTransform;
    private Transform _bowBottomTransform;

    private void Awake()
    {
        _arrowLineRenderer = GameObject.Find("ArrowLine").GetComponent<LineRenderer>();
        _arrowTrajectoryLineRenderer = GameObject.Find("ArrowTrajectoryLine").GetComponent<LineRenderer>();
        _bowStringLineRenderer = GameObject.Find("BowStringLine").GetComponent<LineRenderer>();
        _bowStringLineRenderer2 = GameObject.Find("BowStringLine2").GetComponent<LineRenderer>();

        _arrowTrajectoryLineRenderer.SetPosAllZero();
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
            _arrowTrajectoryLineRenderer.SetPosAllZero();
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
            _bowStringLineRenderer.SetPosAllZero();
            _bowStringLineRenderer2.SetPosAllZero();
            return;
        }

        var arrowControllerPos = VRControllerManager.Instance.ArrowController.CenterTransform.transform.position;
        _bowStringLineRenderer.SetPosition(0, _bowTopTransform.position);
        _bowStringLineRenderer.SetPosition(1, arrowControllerPos);
        _bowStringLineRenderer2.SetPosition(0, arrowControllerPos);
        _bowStringLineRenderer2.SetPosition(1, _bowBottomTransform.position);
    }

    private void ShowArrow()
    {
        if (VRControllerManager.Instance.IsCharging)
        {
            var bowControllerPos = VRControllerManager.Instance.BowController.CenterTransform.position;
            var arrowControllerPos = VRControllerManager.Instance.ArrowController.CenterTransform.position;
            _arrowLineRenderer.SetPosition(0, bowControllerPos);
            _arrowLineRenderer.SetPosition(1,
                Vector3.Lerp(bowControllerPos, arrowControllerPos, VRControllerManager.Instance.ChargingPercent));
        }
        else
        {
            _arrowLineRenderer.SetPosAllZero();
        }
    }

    /// <summary>
    /// 활 각도 변경
    /// </summary>
    private void UpdateRotate()
    {
        if (VRControllerManager.Instance.IsCharging)
        {
            //BowObj.transform.position = VRControllerManager.Instance.BowController.CenterTransform.position;
            /*BowObj.transform.forward = VRControllerManager.Instance.Direction;

            var trackpadPos = VRControllerManager.Instance.BowController.transform.GetChild(0).Find("sys_button").GetChild(0).transform;
            var sysBtnPos = trackpadPos.position;
            BowObj.transform.rotation = trackpadPos.rotation * Quaternion.Euler(180, 0, 180);*/
        }
        else
        {
            //BowObj.transform.localRotation = Quaternion.Euler(70, 0, 0);
        }

        //BowObj.transform.rotation = Quaternion.identity;
        /*var trackpadPos = VRControllerManager.Instance.BowController.transform.GetChild(0).Find("trackpad").GetChild(0).transform.position;
        var sysBtnPos = VRControllerManager.Instance.BowController.transform.GetChild(0).Find("sys_button").GetChild(0).transform.position;
        var direction = trackpadPos - sysBtnPos;
        BowObj.transform.forward = direction;
        BowObj.transform.forward = -BowObj.transform.up;
        BowObj.transform.position = Vector3.Lerp(trackpadPos, sysBtnPos, 0.5f);*/
    }

    
}