using System;
using UnityEngine;

public class BowManager : Singleton<BowManager>
{
    public LineRenderer _arrowTrajectoryLineRenderer;

    private void Awake()
    {
        _arrowTrajectoryLineRenderer = GameObject.Find("ArrowTrajectoryLine").GetComponent<LineRenderer>();
        _arrowTrajectoryLineRenderer.SetPosition(0,new Vector3(0.5f,1,0));
        _arrowTrajectoryLineRenderer.SetPosition(1,new Vector3(0,1,5));
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
        _arrowTrajectoryLineRenderer.SetPosition(0,_arrowTrajectoryLineRenderer.GetPosition(0) + Vector3.forward * Time.deltaTime);
        _arrowTrajectoryLineRenderer.SetPosition(1,_arrowTrajectoryLineRenderer.GetPosition(1) + Vector3.forward * Time.deltaTime);
    }
    
    
}
