using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    void Start()
    {
        maxHealth = 1000f;
        currentHealth = maxHealth;
    }

    public void FlyingStart()
    {
        Debug.Log("test");
    }
}
