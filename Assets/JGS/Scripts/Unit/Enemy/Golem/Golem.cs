using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy
{
    [SerializeField]
    private Transform _shootPosTransfrom;
    [SerializeField]
    private GameObject _projectile;

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
        _targetFloor = Random.RandomRange(0, 2);
        _targetPos = (PlayerFloor.Instance.floorTransforms[_targetFloor].position + PlayerFloor.Instance.floorTransforms[_targetFloor +1 ].position) * 0.5f + new Vector3(0, 0, 3);
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor));
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor + 1));
    }

    public void DisableTargetFloor()
    {
       PlayerFloor.Instance.StopAttack(_targetFloor);
       PlayerFloor.Instance.StopAttack(_targetFloor + 1);
        StopTargeting();
    }

    public void StopTargeting()
    {
        _isTargeted = false;
    }

    private GameObject _stone;

    private void SpawnStone()
    {
        _targetFloor = Random.RandomRange(0, 3);
        _stone = GameObject.Instantiate(_projectile);
        _stone.transform.localPosition = Vector3.zero;
        _stone.GetComponent<Projectile_Stone>()._rootTransform = _shootPosTransfrom;
        _stone.GetComponent<Projectile_Stone>()._targetPos = PlayerFloor.Instance.floorTransforms[_targetFloor].position;
        StartCoroutine(PlayerFloor.Instance.StartAttack(_targetFloor));
    }

    public void ThrowStone()
    {
        _stone.GetComponent<Projectile_Stone>().isThrow = true;
        _stone.GetComponent<Projectile_Stone>().targetFloor = _targetFloor;
    }
}
