using UnityEngine;

public class Stage1 : Stage
{
    private SpawnerManager _spawnerManager;
    public override void StageStart()
    {
        Debug.Log("스테이지1 start");
        _spawnerManager = FindObjectOfType<SpawnerManager>();
    }

    public override void StageUpdate()
    {
        Debug.Log("스테이지1");
    }
}