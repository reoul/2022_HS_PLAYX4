using UnityEngine;

public class Unit : MonoBehaviour
{

    protected float maxHealth = 100;
    protected float currentHealth;

    protected Unit()
    {
        currentHealth = maxHealth;
    }

    public virtual void Damage(float damage)
    {
        currentHealth -= damage;
    }
}