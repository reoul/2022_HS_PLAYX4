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
    public StageType CurStageType => _curStageType;

    public Stage CurStage => stages[(int) _curStageType].GetComponent<Stage>();
    
    public int LastTime { get; private set; }

    public GameObject NextStageObj { get; private set; }
    public GameObject GameExitObj { get; private set; }

    public void Start()
    {
        NextStageObj = FindObjectOfType<IntroObj>().gameObject;
        GameExitObj = FindObjectOfType<GameExitObj>().gameObject;
        stages[(int)StageType.Stage1].GetComponent<Stage>().GoalScore = DataManager.Instance.Data.Stage1TargetScore;
        stages[(int)StageType.Stage1].GetComponent<Stage>().LimitTime = DataManager.Instance.Data.Stage1TimeLimit;
        stages[(int)StageType.Stage2].GetComponent<Stage>().GoalScore = DataManager.Instance.Data.Stage2TargetScore;
        stages[(int)StageType.Stage2].GetComponent<Stage>().LimitTime = DataManager.Instance.Data.Stage2TimeLimit;
        stages[(int)StageType.Stage3].GetComponent<Stage>().LimitTime = DataManager.Instance.Data.Stage3TimeLimit;
        //GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void NextStage()
    {
        Debug.Log("Next Stage");
        if (CurStage.gameObject.activeInHierarchy)
        {
            CurStage.StageEnd();
            CurStage.gameObject.SetActive(false);
        }
        _curStageType = (StageType) (((int) _curStageType + 1) % Enum.GetValues(typeof(StageType)).Length);
        CurStage.IsFinish = false;
        SetUpStage(_curStageType);
    }

    public void ChangeToEnding()
    {
        StopAllCoroutines();
        CurStage.gameObject.SetActive(false);
        _curStageType = StageType.Ending;
        CurStage.StageStart();
    }

    private void Update()
    {
        // todo : 입력키 지우기
        if (Input.GetKeyDown(KeyCode.Y))
        {
            NextStageObj.SetActive(false);
            NextStage();
        }

        CurStage.StageUpdate();
    }


    private void SetUpStage(StageType type)
    {
        // 홀로그램 시작
        CurStage.gameObject.SetActive(true);
        CurStage.StageStart();
        StartHologram(type);
    }

    private void StartHologram(StageType type)
    {
        stages[(int) type].GetComponent<Stage>().StageSetUP();
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        //CurStage.StageEnd();
    }

    public IEnumerator TimerCoroutine(int time)
    {
        LastTime = time;
        if (_curStageType == StageType.Ending)
        {
            yield break;
        }

        for (int i = time; i >= 0; i--)
        {
            TimerText.text = $"TIME : {i} S";
            yield return new WaitForSeconds(1f);
            LastTime = i;
            if (CurStage.IsFinish)
            {
                break;
            }
        }

        CurStage.StageEnd();
        yield return new WaitForSeconds(2f);

        CurStage.gameObject.SetActive(false);

        //yield return new WaitForSeconds(1f);
        Debug.Log("코루틴 next");
        NextStage();
    }
}