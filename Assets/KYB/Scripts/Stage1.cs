using UnityEngine;

public class Stage1 : Stage
{
    private SpawnerManager _spawnerManager;
    
    public override void StageStart()
    {
        base.StageStart();
        _spawnerManager = transform.GetChild(0).GetComponent<SpawnerManager>();
        _spawnerManager.SpawnerAwake();
    }

    public override void StageUpdate()
    {
        base.StageUpdate();
        _spawnerManager.SpawnerUpdate();
    }

    public override void RemoveEnemy()
    {
        base.RemoveEnemy();
        var monsters = FindObjectsOfType<TreeSpirit>();
        foreach (TreeSpirit monster in monsters)
        {
            monster.MoveSpeed = 0;
            var dissolveMat = monster.GetComponentInChildren<DissolveMat>();
            dissolveMat.StartDestroyDissolve();
        }
    }
}