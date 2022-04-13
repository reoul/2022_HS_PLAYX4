using UnityEngine;

public class Stage3 : Stage
{
    public override void StageStart()
    {
        base.StageStart();
        
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
    }
}