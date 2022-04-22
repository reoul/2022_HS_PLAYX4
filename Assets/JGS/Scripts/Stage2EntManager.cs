using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2EntManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] _ents;

    private float _attackDelay;
    private float _lastAttackTime;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            _ents[i].GetComponent<JGS_Ent>().targetFloor = i;
        }
        _attackDelay = 5f;
    }

    private void OnEnable()
    {
        _lastAttackTime = Time.time;
        RandomWeak();
    }

    private void Update()
    {
        if (Time.time - _lastAttackTime > _attackDelay)
        {
            SetTarget();
            _lastAttackTime = Time.time;
        }
    }

    private void SetTarget()
    {
        int[] _randTaget = new int[2];
        _randTaget[0] = Random.RandomRange(0, 3);
        _randTaget[1] = _randTaget[0];
        while (_randTaget[0] == _randTaget[1])
        {
            _randTaget[1] = Random.RandomRange(0, 3);
        }
        int _attackCount;
        _attackCount = Random.RandomRange(1, 3);
        for(int i= 0; i < _attackCount; i++)
        {
            Debug.Log(_ents[_randTaget[i]]);
            _ents[_randTaget[i]].GetComponent<JGS_EntState>().Attack();
            //StartCoroutine(PlayerFloor.Instance.StartAttack(_randTaget[i]));
            //Debug.Log("pass");
        }
    }


    private Transform _curWeak;
    [SerializeField] private Transform[] _weakPoints;
    [SerializeField] private float _weakAlphaSpeed;

    private void RandomWeak()
    {
        _curWeak = _weakPoints[Random.Range(0, _weakPoints.Length)];
        _curWeak.gameObject.SetActive(true);
        _curWeak.GetComponent<WeakPoint>().Show(3);
    }

    /// <summary>
    /// 약점을 변경한다
    /// </summary>
    public void ChangeWeakPoint()
    {
        int rand;
        do
        {
            rand = Random.Range(0, _weakPoints.Length);
        } while (_curWeak == _weakPoints[rand]);

        _curWeak.gameObject.SetActive(false);
        _weakPoints[rand].gameObject.SetActive(true);
        _curWeak = _weakPoints[rand];
        _curWeak.GetComponent<WeakPoint>().Show(_weakAlphaSpeed);
    }
    public void HideWeak()
    {
        _curWeak.gameObject.SetActive(false);
    }
}
