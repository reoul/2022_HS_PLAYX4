using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : Singleton<StageManager>
{
    public enum StageType
    {
        Intro,
        Stage1Before,
        Stage1,
        Stage2Before,
        Stage2,
        Stage3Before,
        Stage3,
        Ending
    }

    [SerializeField] private GameObject[] stages;

    public Text TimerText;

    private StageType _curStageType = StageType.Intro;

    public Stage _curStage => stages[(int) _curStageType].GetComponent<Stage>();

    public GameObject _nextStageObj;

    public void Start()
    {
        _nextStageObj = FindObjectOfType<IntroObj>().gameObject;
        stages[(int)StageType.Stage1].GetComponent<Stage>().GoalScore = DataManager.Instance.Data.Stage1TargetScore;
        stages[(int)StageType.Stage1].GetComponent<Stage>().LimitTime = DataManager.Instance.Data.Stage1TimeLimit;
        stages[(int)StageType.Stage2].GetComponent<Stage>().GoalScore = DataManager.Instance.Data.Stage2TargetScore;
        stages[(int)StageType.Stage2].GetComponent<Stage>().LimitTime = DataManager.Instance.Data.Stage2TimeLimit;
        stages[(int)StageType.Stage3].GetComponent<Stage>().LimitTime = DataManager.Instance.Data.Stage3TimeLimit;
        //GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void NextStage()
    {
        if (_curStage.gameObject.activeInHierarchy)
        {
            _curStage.StageEnd();
            _curStage.gameObject.SetActive(false);
        }
        _curStageType = (StageType) (((int) _curStageType + 1) % Enum.GetValues(typeof(StageType)).Length);
        Debug.Log("Next Stage11");
        ScoreSystem.SumScore += ScoreSystem.Score;
        ScoreSystem.Score = 0;
        SetUpStage(_curStageType);
    }

    public void ChangeToEnding()
    {
        StopAllCoroutines();
        _curStage.StageEnd();
        _curStage.gameObject.SetActive(false);
        _curStageType = StageType.Ending;
        _curStage.StageStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _nextStageObj.SetActive(false);
            NextStage();
        }

        _curStage.StageUpdate();
    }


    private void SetUpStage(StageType type)
    {
        // 홀로그램 시작
        _curStage.gameObject.SetActive(true);
        _curStage.StageStart();
        StartHologram(type);
    }

    private void StartHologram(StageType type)
    {
        stages[(int) type].GetComponent<Stage>().StageSetUP();
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        _curStage.StageEnd();
    }

    public IEnumerator TimerCoroutine(int time)
    {
        if (_curStageType == StageType.Ending)
        {
            yield break;
        }

        for (int i = time; i >= 0; i--)
        {
            TimerText.text = $"TIME : {i} S";
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Next Stage1");
        _curStage.StageEnd();
        Debug.Log("Next Stage2");
        yield return new WaitForSeconds(2f);

        _curStage.gameObject.SetActive(false);
        Debug.Log("Next Stage3");

        //yield return new WaitForSeconds(1f);
        Debug.Log("Next Stage4");
        NextStage();
    }
}