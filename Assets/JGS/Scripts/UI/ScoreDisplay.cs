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
        if (Input.GetKeyDown(KeyCode.B))
        {
            switchDisplay();
        }
    }

    private void switchDisplay()
    {
        if (_isBossStage)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 300);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 500);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        _isBossStage = !_isBossStage;
    }
}
