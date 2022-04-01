using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disolveSpider : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;

    private Material material;
    private float height_two = -999f;
    private bool _isCreate = false;
    private bool _isDelete = false;

    private float _disolveSpeed = 20;

    private float _startTime;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    public void InitTime()
    {
        _startTime = Time.time;
        _isCreate = false;
        _isDelete = false;
    }

    public bool Disolve()
    {
        if (height_two <= (1.8f + transform.position.y) && !_isCreate ) 
        {
            var time = (Time.time - _startTime) * Mathf.PI * Time.deltaTime * _disolveSpeed;
            //var time = (Time.time - _startTime) * Mathf.PI * 0.15f;

            float height = transform.position.y - gameObject.GetComponentInParent<Collider>().bounds.size.y * 0.5f;

            height += Mathf.Sin(time) * objectHeight * 5;
            height_two = height;
            SetHeight(height);
            return false;
        }
        else
        {
            _isCreate = true;
            height_two += 1;
            return true;
        }
    }

    public void InitAA()
    {
        height_two = transform.position.y + gameObject.GetComponentInParent<Collider>().bounds.size.y * 0.5f;
    }

    public bool DeleteDisolve()
    {
        if (height_two >= 0 && !_isDelete ) 
        {
            var time = (Time.time - _startTime) * Mathf.PI * 0.005f;
            //float height = transform.position.y;
            //Debug.Log(height);
            height_two -= Mathf.Sin(time) * objectHeight * 5;

            //Debug.Log(height_two);

            SetHeight(height_two);
            return false;
        }
        else
        {
            _isDelete = true;
            height_two -= 2;
            return true;
        }
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}
