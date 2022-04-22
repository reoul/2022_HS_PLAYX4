using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGS_Ent : Enemy
{
    [SerializeField] private Transform _shootPosTransfrom;
    [SerializeField] private GameObject _projectile;

    private JGS_EntState _state;
    private Vector3 _startPos;

    public int targetFloor;

    private void Start()
    {
        _state = this.GetComponent<JGS_EntState>();
        _startPos = transform.position;
    }

    private void Update()
    {
        //if(state == null)
        //{
        //    state = this.GetComponent<JGS_EntState>();
        //}

        //if (_state.IsIdle())
        //{
        //    transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, 0.1f);
        //}
    }

    private GameObject _stone;

    private void SpawnStone()
    {
        _stone = GameObject.Instantiate(_projectile);
        _stone.transform.localPosition = Vector3.zero;
        _stone.GetComponent<Projectile_Stone>()._rootTransform = _shootPosTransfrom;
        _stone.GetComponent<Projectile_Stone>()._targetPos = PlayerFloor.Instance.attackTrans[targetFloor].position;
        StartCoroutine(PlayerFloor.Instance.StartAttack(targetFloor));
    }

    public void ThrowStone()
    {
        _stone.GetComponent<Projectile_Stone>().isThrow = true;
        _stone.GetComponent<Projectile_Stone>().targetFloor = targetFloor;
    }
}
