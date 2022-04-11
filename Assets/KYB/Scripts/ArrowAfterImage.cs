using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAfterImage : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private Vector3 _direction;
    private bool _isShot = false;

    private float _speed = 1;
    private float _alphaSpeed = 1;

    private void Start()
    {
        _lineRenderer = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Effect1();
    }

    private void Effect1()
    {
        if (_isShot)
        {
            _lineRenderer.SetPosition(1, _lineRenderer.GetPosition(1) + (_direction * _speed));
            _speed *= 70 * Time.deltaTime;
            _alphaSpeed *= 1 + Time.deltaTime;

            _lineRenderer.material.color -= Color.black * _alphaSpeed * Time.deltaTime;
            if (_lineRenderer.material.color.a <= 0)
            {
                _isShot = false;
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, Vector3.zero);
            }
        }
    }

    private void Effect2()
    {
        if (_isShot)
        {
            Debug.Log("asdasdasd");
            _speed *= 1 + Time.deltaTime;
            _lineRenderer.material.color -= Color.black * _alphaSpeed * Time.deltaTime;
            if (_lineRenderer.material.color.a <= 0)
            {
                _isShot = false;
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, Vector3.zero);
            }
        }
    }

    public void Shot()
    {
        _speed = 1;
        _lineRenderer.SetPosition(0, VRControllerManager.Instance.Direction * 10000);
        _lineRenderer.SetPosition(1, VRControllerManager.Instance.BowController.transform.position);
        _lineRenderer.material.color = Color.white;
        _direction = VRControllerManager.Instance.Direction.normalized;
        _isShot = true;
    }
}