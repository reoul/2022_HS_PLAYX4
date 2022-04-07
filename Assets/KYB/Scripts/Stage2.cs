using UnityEngine;

public class Stage2 : Stage
{
    public override void StageStart()
    {
        StartCoroutine(PlayerFloor.Instance.Hit(10));
        //PlayerFloor.Instance.
    }

    public override void StageUpdate()
    {
        Debug.Log("스테이지2");
    }
}