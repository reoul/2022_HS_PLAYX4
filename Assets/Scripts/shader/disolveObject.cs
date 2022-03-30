﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disolveObject : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;

    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        var time = Time.time * Mathf.PI * 0.5f;

        float height = transform.position.y;
        height += Mathf.Sin(time) * objectHeight * 8 ;
        SetHeight(height);
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}
