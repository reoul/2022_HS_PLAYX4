using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : Singleton<StageManager>
{
    public enum StageType
    {
        Intro,
        Stage1,
        Stage2,
        Stage3,
        Ending
    }

    [SerializeField] private GameObject[] stages;

    public Text TimerText;

    private StageType _curStageType = StageType.Intro;

    private Stage _curStage => stages[(int) _curStageType - 1].GetComponent<Stage>();

    private void NextStage()
    {
        _curStageType = (StageType) (((int) _curStageType + 1) % Enum.GetValues(typeof(StageType)).Length);
        ScoreSystem.Score = 0;
        SetUpStage(_curStageType);
        StartCoroutine(TimerCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            NextStage();
        }

        if (_curStageType >= StageType.Stage1 && _curStageType <= StageType.Stage3)
        {
            _curStage.StageUpdate();
        }
    }


    private void SetUpStage(StageType type)
    {
        // 홀로그램 시작
        if (type >= StageType.Stage1 && type <= StageType.Stage3)
        {
            StartHologram(type);
            _curStage.gameObject.SetActive(true);
            _curStage.StageStart();
        }
        // 조건문 : 만약 홀로그램이 다 끝났다면
    }

    private void StartHologram(StageType type)
    {
        stages[(int) type - 1].GetComponent<Stage>().StageSetUP();
    }

    IEnumerator TimerCoroutine()
    {
        if (_curStageType == StageType.Ending)
        {
            yield break;
        }

        for (int i = 20; i >= 0; i--)
        {
            TimerText.text = $"남은 시간 : {i}초";
            yield return new WaitForSeconds(1f);
        }
        _curStage.RemoveStage();
        yield return new WaitForSeconds(1f);
        
        _curStage.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);
        
        NextStage();
    }
}