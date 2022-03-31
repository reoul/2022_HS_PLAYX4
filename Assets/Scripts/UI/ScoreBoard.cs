using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    private int _currentScore;
    private float _displayScore;

    private void Start()
    {
        _currentScore = 0;
        _displayScore = _currentScore;
    }

    private void Update()
    {
        ScoreSystem.DebugAddScore();
        _currentScore = ScoreSystem.score;
        UpdateDisplayScore();
        this.GetComponent<Text>().text = Mathf.Round(_displayScore).ToString();
    }

    private void UpdateDisplayScore()
    {
        _displayScore = Mathf.Lerp(_displayScore, _currentScore, 0.1f);
    }

    public int AddCurrentScore
    {
        get { return _currentScore; }
        set { _currentScore += value; }
    }
    public int SubtractCurrentScore
    {
        get { return _currentScore; }
        set { _currentScore -= value; }
    }
}
