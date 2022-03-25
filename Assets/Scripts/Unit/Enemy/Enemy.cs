using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public enum EnemyType { None,stage1,stage2,stage3 };

    public string name { get; private set; }
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    public Enemy(string tName, float tMaxHealth)
    {
        this.name = tName;
        this.maxHealth = tMaxHealth;
        this.currentHealth = tMaxHealth;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    override public void Damage(float damage)
    {
        OnDamage();
    }

    private void OnDamage() { }
}
