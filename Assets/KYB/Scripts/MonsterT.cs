using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterT : MonoBehaviour
{
    public Transform[] weakPoints;

    private Transform _curWeak;

    public void Start()
    {
        RandomWeak();
    }

    private void RandomWeak()
    {
        _curWeak = weakPoints[Random.Range(0, weakPoints.Length)];
        _curWeak.gameObject.SetActive(true);
    }

    public void ChangeWeakPoint()
    {
        int rand;
        do
        {
            rand = Random.Range(0, weakPoints.Length);
        } while (_curWeak == weakPoints[rand]);
        _curWeak.gameObject.SetActive(false);
        weakPoints[rand].gameObject.SetActive(true);
        _curWeak = weakPoints[rand];
    }
}