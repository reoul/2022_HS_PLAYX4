using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSystem
{
    public static int score { get; set; }

    static ScoreSystem()
    {
        score = 0;
    }

    public static void DebugAddScore()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += 100;
            Debug.Log(score);
        }
    }

    public static void StageClear()
    {

    }
}