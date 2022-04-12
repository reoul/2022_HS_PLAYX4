using UnityEngine;

public class Stage3 : Stage
{
    public override void StageStart()
    {
        base.StageStart();
        StartCoroutine(PlayerFloor.Instance.Hit(19));
        
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
    }
}