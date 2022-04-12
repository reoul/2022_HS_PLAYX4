using UnityEngine;

public class Stage1 : Stage
{
    private SpawnerManager _spawnerManager;
    
    public override void StageStart()
    {
        base.StageStart();
        _spawnerManager = transform.GetChild(0).GetComponent<SpawnerManager>();
    }

    public override void StageUpdate()
    {
        base.StageUpdate();
        _spawnerManager.SpawnUpdate();
    }

    public override void RemoveEnemy()
    {
        base.RemoveEnemy();
        var monsters = FindObjectsOfType<StrategySpider>();
        foreach (StrategySpider monster in monsters)
        {
            var dissolveMat = monster.GetComponentInChildren<DissolveMat>();
            dissolveMat.StartDestroyDissolve(CheckFinishDissolveAll);
        }
    }
}