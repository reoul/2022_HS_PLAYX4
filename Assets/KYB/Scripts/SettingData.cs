public class SettingData
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
        BowMaxChargingTime = 60f;
        StartChargingDistanceMin = 0.3f;
        ChargingMaxDistance = 0.5f;
        WendigoScore = 100;
        EntScore = 100;
        GolemScore = 100;
        PlayerMaxHP = 15;
        EntWeakAttackBreakCnt = 3;
        EntWeakAttackBreakTime = 3;
        GolemWeakAttackBreakCnt = 3;
        GolemWeakAttackBreakTime = 3;
        PlayerExitTimeInterval = 1;
        IntroBowSpawnPosZ = 0f;
        GolemAttackFollowPlayer = false;
    }
}
