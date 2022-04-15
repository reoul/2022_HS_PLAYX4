using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    private bool _isGameStart;
    
    // Use this for initialization
    private void Start ()
    {
        _isGameStart = false;
        
        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 0;
        RenderSettings.fogEndDistance = 5;

    }

    private void Update () {
        if (_isGameStart)
        {
            RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 100, Time.deltaTime * 2.5f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameStart();
        }
    }

    public void GameStart()
    {
        _isGameStart = true;
    }
}
