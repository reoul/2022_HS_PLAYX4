using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy
{
    private Transform _target;

    private int _targetFloor;

    private void Start()
    {
        _target = GameObject.Find("[CameraRig]").transform;
    }

    public void SetTargetFloor()
    {
        _targetFloor = Random.RandomRange(0, 3);
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor));
        this.transform.position = PlayerFloor.Instance.floorTransforms[_targetFloor].position;
    }

    public void DisableTargetFloor()
    {
       PlayerFloor.Instance.StopAttack(_targetFloor);
    }
}
