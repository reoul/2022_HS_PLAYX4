using UnityEngine;

public class Stage2 : Stage
{
    public override void StageStart()
    {
        base.StageStart();
        StartCoroutine(PlayerFloor.Instance.Hit(19));
    }
}