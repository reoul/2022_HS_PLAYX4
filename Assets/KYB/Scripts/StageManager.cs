using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    [SerializeField]
    private GameObject[] stages;

    private StageType _currentStage = StageType.Intro;

    private void NextStage()
    {
        _currentStage = (StageType)(((int)_currentStage + 1) % Enum.GetValues(typeof(StageType)).Length);
        SetUpStage(_currentStage);
    }

    public Stage stage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            stage.StageSetUP();
        }
    }


    private void SetUpStage(StageType type)
    {
        // 홀로그램 시작
        StartHologram();
        // 조건문 : 만약 홀로그램이 다 끝났다면
        
        /*foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(stages[_currentStage - 1],transform);*/
    }

    private void StartHologram()
    {
        
    }
}
