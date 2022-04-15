﻿using System;
using UnityEngine;

public class IntroBow : MonoBehaviour
{
    private float _time = 0;
    private void Start()
    {
        _time = Time.time;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        this.transform.localPosition = new Vector3(0, Mathf.Sin(_time) * 0.1f, 0);
        this.transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }
}