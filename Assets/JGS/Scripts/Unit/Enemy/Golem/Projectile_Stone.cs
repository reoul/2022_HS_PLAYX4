﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Stone : MonoBehaviour
{
    public Vector3 _targetPos;
    public Transform _rootTransform;
    public int targetFloor;

    private float _scale;
    public bool isThrow;

    void Start()
    {
        if(_targetPos == null)
        {
            _targetPos = Vector3.zero;
        }
        transform.localScale = Vector3.zero;
        _scale = 0;
        isThrow = false;
    }

    void Update()
    {
        if (_scale < 1)
        {
            _scale = Mathf.Lerp(_scale, 1,0.1f);
            transform.localScale = new Vector3(_scale, _scale, _scale);
        }
        if (isThrow)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, 35 * Time.deltaTime);
        }
        else
        {
            this.transform.position = _rootTransform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFloor"))
        {
            PlayerFloor.Instance.StopAttack(targetFloor);
            Destroy(gameObject);
        }
    }
}
