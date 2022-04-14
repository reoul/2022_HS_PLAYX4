﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floor : MonoBehaviour
{
    public SpriteRenderer[] childRenderers;

    public void ChangeColor(Color color)
    {
        foreach (SpriteRenderer renderer in childRenderers)
        {
            renderer.color = color;
        }
    }
}
