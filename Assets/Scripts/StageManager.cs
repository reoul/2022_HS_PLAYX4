using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField]
    private GameObject[] stages;

    private int currentStage = 1;

    private void NextStage()
    {
        if(++currentStage > stages.Length)
        {
            currentStage = 1;
        }
        setupStage();
    }

    private void setupStage()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(stages[currentStage - 1],transform);
    }

    private void Update() //디버깅용 :: 오른쪽 화살표 버튼 입력시 다음 스테이지 세팅
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextStage();
        }
    }
}
