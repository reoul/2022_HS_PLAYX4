using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSystem
{
    public static int Score { get; set; }

    static ScoreSystem()
    {
        Score = 0;
    }
}