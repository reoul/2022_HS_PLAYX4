using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    //Enemy 초기화
    public enum EnemyType { None, stage1, stage2, stage3 };

    public string name { get; private set; }
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    public Enemy()
    {

    }


    //피격 판정
    override public void Damage(float damage)
    {
        OnDamage();
    }

    private void OnDamage() { }

    //UnityEngine

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
}

