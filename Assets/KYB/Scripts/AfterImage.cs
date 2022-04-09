using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private Vector3 _direction;
    private bool _isShot = false;

    private float _speed = 1;

    private void Start()
    {
        _lineRenderer = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(_isShot)
        {
            _lineRenderer.SetPosition(1, _lineRenderer.GetPosition(1) + (_direction * _speed));
            _speed *= 100 * Time.deltaTime;
            if(_speed >= 1000)
            {
                _isShot = false;
            }
        }
    }

    public void Shot()
    {
        _speed = 1;
        _lineRenderer.SetPosition(0, VRControllerManager.Instance.Direction * 10000);
        _lineRenderer.SetPosition(1, VRControllerManager.Instance.BowController.transform.position);
        _direction = VRControllerManager.Instance.Direction.normalized;
        _isShot = true;
    }
}
