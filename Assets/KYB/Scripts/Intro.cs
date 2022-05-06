using System;
using UnityEngine;
using UnityEngine;

public class Intro : Stage
{
    private void Start()
    {
        Invoke("StageStart", 0.5f);
    }

    public override void StageStart()
    {
        base.StageStart();
        StageManager.Instance._nextStageObj.SetActive(true);
        ScoreSystem.Init();
        //GameObject.Find("ScoreDisplay").transform.GetChild(1).gameObject.SetActive(false);
        BowManager.Instance.Init();
    }
}