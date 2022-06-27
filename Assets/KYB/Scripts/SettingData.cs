﻿public class SettingData
{
    //====== Wendigo ======
    public int WendigoMaxSpawnCount;
    public float WendigoSpawnDelay;
    public float WendigoMoveSpeed;
    public int Stage1TargetScore;
    public int Stage1TimeLimit;
    public int WendigoDamage;
    public int WendigoScore;
    //====== Ent ======
    public float EntAttackDelay;
    public int Stage2TargetScore;
    public int Stage2TimeLimit;
    public int EntDamage;
    public int EntScore;
    public int EntWeakAttackBreakCnt;
    public float EntWeakAttackBreakTime;
    //====== Golem ======
    public int GolemMaxHealth;
    public int Stage3TimeLimit;
    public int GolemDamage;
    public int GolemScore;
    public int GolemWeakAttackBreakCnt;
    public float GolemWeakAttackBreakTime;
    public bool GolemAttackFollowPlayer;
    //====== Player ======
    public float FloorWidth;
    public float BowMaxChargingTime;
    public float StartChargingDistanceMin;
    public int PlayerMaxHP;
    public float PlayerExitTimeInterval;
    public float IntroBowSpawnPosZ;
    public float ChargingMaxDistance;

    public SettingData()
    {
        WendigoSpawnDelay = 0.5f;
        WendigoMaxSpawnCount = 3;
        WendigoMoveSpeed = 3.8f;
        WendigoDamage = 1;
        WendigoScore = 15;
        EntAttackDelay = 5;
        EntDamage = 3;
        EntScore = 10;
        GolemMaxHealth = 15;
        GolemDamage = 20;
        GolemScore = 7;
        FloorWidth = 0.4f;
        Stage1TargetScore = 150;
        Stage2TargetScore = 120;
        Stage1TimeLimit = 160;
        Stage2TimeLimit = 90;
        Stage3TimeLimit = 210;
        BowMaxChargingTime = 46;
        StartChargingDistanceMin = 0.35f;
        ChargingMaxDistance = 0.4f;
        PlayerMaxHP = 15;
        EntWeakAttackBreakCnt = 3;
        EntWeakAttackBreakTime = 3;
        GolemWeakAttackBreakCnt = 3;
        GolemWeakAttackBreakTime = 3;
        PlayerExitTimeInterval = 1;
        IntroBowSpawnPosZ = 0;
        GolemAttackFollowPlayer = true;
    }
}
