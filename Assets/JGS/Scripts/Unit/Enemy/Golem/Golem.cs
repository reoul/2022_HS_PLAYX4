using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Golem : Enemy
{
    [SerializeField] private Transform _shootPosTransfrom;
    [SerializeField] private GameObject _projectile;

    public DissolveMat WeaponDissolveMat;

    private Transform _target;

    private int _targetFloor;

    private float _time;
    private bool _isMoveForward = false;

    [SerializeField] private float _weakAlphaSpeed;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
    }

    private void Start()
    {
        _target = GameObject.Find("[CameraRig]").transform;
        RandomWeak();
    }

    private void Update()
    {
        _stateMachine.StateUpdate();
    }

    public void ChangeState(GolemState.StateType state)
    {
        _stateMachine.ChangeState(_stateMachine.StateDictionary[(int)state]);
    }

    private Transform _curWeak;
    [SerializeField] private Transform[] _weakPoints;

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

    private bool _isTargeted;

    private GameObject _stone;

    private void SpawnStone()
    {
        _targetFloor = Random.RandomRange(0, 3);
        _stone = GameObject.Instantiate(_projectile);
        _stone.transform.localPosition = Vector3.zero;
        _stone.GetComponent<Projectile_Stone>()._rootTransform = _shootPosTransfrom;
        _stone.GetComponent<Projectile_Stone>()._targetPos = PlayerFloor.Instance.attackTrans[_targetFloor].position;
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor));
    }

    public void ThrowStone()
    {
        _stone.GetComponent<Projectile_Stone>().isThrow = true;
        _stone.GetComponent<Projectile_Stone>().targetFloor = _targetFloor;
    }

    public void HideWeak()
    {
        _curWeak.gameObject.SetActive(false);
    }
}