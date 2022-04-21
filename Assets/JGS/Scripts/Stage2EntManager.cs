using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2EntManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] _ents;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetTarget();
        }
    }

    private void SetTarget()
    {
        int[] _randTaget = new int[2];
        _randTaget[0] = Random.RandomRange(0, 3);
        _randTaget[1] = _randTaget[0];
        while (_randTaget[0] != _randTaget[1])
        {
            _randTaget[1] = Random.RandomRange(0, 3);
        }
        int _attackCount;
        _attackCount = Random.RandomRange(0, 2);
        for(int i= 0; i < _attackCount; i++)
        {
            StartCoroutine(PlayerFloor.Instance.StartAttack(_randTaget[i]));
            Debug.Log("pass");
        }
    }
}
