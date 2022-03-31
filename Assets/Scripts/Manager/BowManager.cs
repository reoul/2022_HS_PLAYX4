using System;
using UnityEngine;

public class BowManager : Singleton<BowManager>
{
    public LineRenderer _arrowTrajectoryLineRenderer;

    private void Awake()
    {
        _arrowTrajectoryLineRenderer = GameObject.Find("ArrowTrajectoryLine").GetComponent<LineRenderer>();
        _arrowTrajectoryLineRenderer.SetPosition(0, Vector3.zero);
        _arrowTrajectoryLineRenderer.SetPosition(1, Vector3.zero);
    }

    private void Update()
    {
        ShowArrowTrajectory();
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
        _arrowTrajectoryLineRenderer.SetPosition(0, VRControllerManager.Instance.BowController.gameObject.transform.position);
        _arrowTrajectoryLineRenderer.SetPosition(1, VRControllerManager.Instance.Direction * 1000);
    }
}