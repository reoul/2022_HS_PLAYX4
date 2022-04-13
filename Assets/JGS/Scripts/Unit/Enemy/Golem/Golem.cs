using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy
{
    private Transform _target;

    private int _targetFloor;

    private Vector3 _startPos;

    private Vector3 _targetPos;

    private void Start()
    {
        _target = GameObject.Find("[CameraRig]").transform;
        _startPos = transform.position;
    }

    private void Update()
    {
        if (_isTargeted)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPos, 0.05f);
        }
    }

    private bool _isTargeted;

    public void MoveToTarget()
    {
        _isTargeted = true;
    }

    public void SetTargetZero()
    {
        _targetPos = _startPos;
        _isTargeted = true;
    }

    public void SetTargetFloor()
    {
        _targetFloor = Random.RandomRange(0, 3);
        _targetPos = PlayerFloor.Instance.floorTransforms[_targetFloor].position + new Vector3(0, 0, 2);
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor));
    }

    public void DisableTargetFloor()
    {
       PlayerFloor.Instance.StopAttack(_targetFloor);
        StopTargeting();
    }

    public void StopTargeting()
    {
        _isTargeted = false;
    }
    
}
