﻿using UnityEngine;

public class Ending : Stage
{
    public override void StageStart()
    {
        StageManager.Instance._nextStageObj.SetActive(true);
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
    }

    public override void StageUpdate()
    {
        
    }
}