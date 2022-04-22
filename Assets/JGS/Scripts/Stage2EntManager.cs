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
        _lastAttackTime = Time.time;
    }

    private void Update()
    {
        Debug.Log(Time.time - _lastAttackTime);
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
}
