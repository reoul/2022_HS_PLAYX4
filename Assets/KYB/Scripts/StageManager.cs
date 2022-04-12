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

    public Stage _curStage => stages[(int) _curStageType].GetComponent<Stage>();

    public GameObject _nextStageObj;

    public void Start()
    {
        _nextStageObj = FindObjectOfType<IntroObj>().gameObject;
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void NextStage()
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

        _curStage.StageUpdate();
    }


    private void SetUpStage(StageType type)
    {
        // 홀로그램 시작
        StartHologram(type);
        _curStage.gameObject.SetActive(true);
        _curStage.StageStart();
        // 조건문 : 만약 홀로그램이 다 끝났다면
    }

    private void StartHologram(StageType type)
    {
        stages[(int) type].GetComponent<Stage>().StageSetUP();
    }

    IEnumerator TimerCoroutine()
    {
        if (_curStageType == StageType.Ending)
        {
            yield break;
        }

        for (int i = 5; i >= 0; i--)
        {
            TimerText.text = $"남은 시간 : {i}초";
            yield return new WaitForSeconds(1f);
        }

        _curStage.RemoveStage();
        yield return new WaitForSeconds(5f);

        //_curStage.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);

        //NextStage();
    }
}