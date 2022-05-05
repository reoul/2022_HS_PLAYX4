﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField] private Transform[] _rankP;
    [SerializeField] private Transform _myRank;
    
    private List<Score> _ranking;
    
    private void Start()
    {
        _ranking = DataManager.Instance.GetScore();
        UpdateScoreBoard();
    }

    private void UpdateScoreBoard()
    {
        Score newScore = new Score("NONE",0);
        if (ScoreSystem.SumScore > 0)
        {
            newScore = DataManager.Instance.SaveNewScore("NewPlayer");
        }
        
        //스코어 순으로 정렬
        _ranking = _ranking.OrderBy(x => x.score).Reverse().ToList();
        
        //스코어보드 출력
        for (int i = 0; i < _rankP.Length; i++)
        {
            _rankP[i].GetChild(1).GetComponent<Text>().text = _ranking[i].name;
            _rankP[i].GetChild(2).GetComponent<Text>().text = _ranking[i].score.ToString();
        }
        
        _myRank.GetChild(0).GetComponent<Text>().text = FindPlayerRank(newScore).ToString();
        _myRank.GetChild(1).GetComponent<Text>().text = newScore.name;
        _myRank.GetChild(2).GetComponent<Text>().text = newScore.score.ToString();
    }

    private int FindPlayerRank(Score myScore)
    {
        int myRank = 0;
        foreach (var score in _ranking.Select((value, index) => (value, index)))
        {
            if (score.value.name == myScore.name && score.value.score == myScore.score)
            {
                myRank = score.index;
            }
        }

        return myRank;
    }
}