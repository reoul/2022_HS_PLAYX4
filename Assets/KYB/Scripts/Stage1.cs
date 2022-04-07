using UnityEngine;

public class Stage1 : Stage
{
    private SpawnerManager _spawnerManager;
    
    public override void StageStart()
    {
        _spawnerManager = transform.GetChild(0).GetComponent<SpawnerManager>();
    }

    public override void RemoveEnemy()
    {
        base.RemoveEnemy();
        //_spawnerManager.spawnerQueue
            
    }

    public override void StageUpdate()
    {
        _spawnerManager.SpawnUpdate();
    }
}