﻿public class SettingData
{
    public int PlayerHp;
    public int BossHp;
    public int BossDamage;
    public float MoveSpeed;
    
    //====== Wendigo ======
    public int WendigoMaxSpawnCount;
    public float WendigoSpawnDelay;
    public float WendigoMoveSpeed;
    public int Stage1TargetScore;
    public int Stage1TimeLimit;
    public int WendigoDamage;
    //====== Ent ======
    public float EntAttackDelay;
    public int Stage2TargetScore;
    public int Stage2TimeLimit;
    public int EntDamage;
    //====== Golem ======
    public int GolemMaxHealth;
    public int Stage3TimeLimit;
    public int GolemDamage;
    //====== Player ======
    public float FloorWidth;
    public float BowMaxChargingTime;

    public SettingData()
    {
        PlayerHp = 100;
        BossHp = 100;
        BossDamage = 13;
        MoveSpeed = 100;
        WendigoSpawnDelay = 0.5f;
        WendigoMaxSpawnCount = 5;
        WendigoMoveSpeed = 1f;
        WendigoDamage = 100;
        EntAttackDelay = 5f;
        EntDamage = 100;
        GolemMaxHealth = 20;
        GolemDamage = 100;
        FloorWidth = 1f;
        Stage1TargetScore = 2000;
        Stage2TargetScore = 4000;
        Stage1TimeLimit = 10;
        Stage2TimeLimit = 10;
        Stage3TimeLimit = 10;
        BowMaxChargingTime = 3f;
    }
}
