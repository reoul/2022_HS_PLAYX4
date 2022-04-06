﻿using System;
using UnityEngine;

public class LSM_ArrowManager : Singleton<LSM_ArrowManager>
{
    public GameObject ArrowPrefab;
    public void Shot(Vector3 positon, Vector3 direction)
    {
        var arrow = GameObject.Instantiate(ArrowPrefab);
        arrow.GetComponent<Arrow>().Init(positon, direction);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Shot(new Vector3(0,1,4),new Vector3(0,0,1));
            SoundManager.Instance.PlaySound("Rock 6", 1f);
        }
        
        
    }
}
