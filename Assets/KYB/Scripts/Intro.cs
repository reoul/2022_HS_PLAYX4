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
        
        // todo : 스코어디스플레이 초기화시켜주기
        //GameObject.Find("ScoreDisplay").transform.GetChild(1).gameObject.SetActive(false);
        BowManager.Instance.Init();
    }
}