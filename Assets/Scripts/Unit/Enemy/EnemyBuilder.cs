using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { monter1, monter2, monter3 }

public class EnemyBuilder
{
    private string name = "defaultName";
    private float maxHealth = 100;
    private float currentHealth = 100;
    private EnemyType enemyType;

    public EnemyBuilder(string tName)
    {
        this.name = tName;
    }

    public EnemyBuilder SetHealth(float tMaxHealth)
    {
        this.maxHealth = tMaxHealth;
        this.currentHealth = tMaxHealth;
        return this;
    }

    public EnemyBuilder SetEnemyType(EnemyType enemyType)
    {
        this.enemyType = enemyType;
        return this;
    }

    public Enemy Build()
    {
        GameObject enemyObj;
        switch (enemyType)
        {
            case EnemyType.monter1:
                enemyObj = GameObject.Instantiate(Resources.Load("Temp_DefaultCubeEnemy", typeof(GameObject))) as GameObject;
                break;
            case EnemyType.monter2:
                enemyObj = GameObject.Instantiate(Resources.Load("Forest_Golem_4_PA", typeof(GameObject))) as GameObject;
                break;
            case EnemyType.monter3:
                enemyObj = GameObject.Instantiate(Resources.Load("Temp_DefaultCubeEnemy", typeof(GameObject))) as GameObject;
                break;
            default:
                enemyObj = GameObject.Instantiate(Resources.Load("Temp_DefaultCubeEnemy", typeof(GameObject))) as GameObject;
                break;
        }
        enemyObj.name = this.name;
        enemyObj.AddComponent<Enemy>();
        return enemyObj.GetComponent<Enemy>();
    }
}
