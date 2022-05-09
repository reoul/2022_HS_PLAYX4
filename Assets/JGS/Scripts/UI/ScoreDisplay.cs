using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private bool _isBossStage;

    private RectTransform rect;
    
    private void Start()
    {
        rect = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Update()
    {
        // todo : 입력키 지우기
        if (Input.GetKeyDown(KeyCode.B))
        {
            switchDisplay();
        }
    }

    public void switchDisplay()
    {
        if (_isBossStage)
        {
            //rect.sizeDelta = new Vector2(rect.sizeDelta.x, 300);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            HealthBarManager.Instance.ActiveBossHP(false);
        }
        else
        {
            //rect.sizeDelta = new Vector2(rect.sizeDelta.x, 500);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            HealthBarManager.Instance.ActiveBossHP(true);
        }
        _isBossStage = !_isBossStage;
    }
}
