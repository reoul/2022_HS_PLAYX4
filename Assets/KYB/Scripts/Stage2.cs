using UnityEngine;

public class Stage2 : Stage
{
    public override void StageStart()
    {
        StartCoroutine(PlayerFloor.Instance.Hit(19));
    }

    public override void StageUpdate()
    {
    }
}