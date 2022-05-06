﻿using UnityEngine;

public class Stage3 : Stage
{
    private Golem _golem;
    private DissolveMat _golemDissolveMat;
    public override void StageStart()
    {
        base.StageStart();
        EnemyInit();
        Invoke("GolemSpawn", 5);
        FindObjectOfType<ScoreDisplay>().switchDisplay();
        StartCoroutine(StageManager.Instance.TimerCoroutine(LimitTime));
        HealthBarManager.Instance.ActiveBossHP(true);
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.BGMChange("Beside Me - Patrick Patrikios", 0.2f);
    }

    public override void StageEnd()
    {
        base.StageEnd();
        RemoveEnemy();
        //FindObjectOfType<ScoreDisplay>().switchDisplay();
        _golem.gameObject.SetActive(false);
        HealthBarManager.Instance.ActiveBossHP(false);
    }

    /// <summary>
    /// 골렘 찾아주고 초기화 해준다
    /// </summary>
    private void EnemyInit()
    {
        _golem = FindObjectOfType<Golem>();
        _golemDissolveMat = _golem.GetComponentInChildren<DissolveMat>();
        _golemDissolveMat.SetDissolveHeightMin();
        _golemDissolveMat.State = DissolveMat.DissolveState.Hide;
        _golem.gameObject.SetActive(false);
    }

    private void GolemSpawn()
    {
        _golemDissolveMat.StartCreateDissolve();
        _golem.gameObject.SetActive(true);
    }

    private void RemoveEnemy()
    {
        _golem.HideWeak();
        _golemDissolveMat.StartDestroyDissolve();
    }
}