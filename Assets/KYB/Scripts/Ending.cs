using UnityEngine;

public class Ending : Stage
{
    public GameObject ScoreDisplay;
    public override void StageStart()
    {
        base.StageStart();
        ScoreDisplay = GameObject.Find("ScoreDisplay");
        ScoreDisplay.SetActive(false);
    }

    public override void StageUpdate()
    {
        
    }
    
    public override void StageEnd()
    {
        base.StageEnd();
        ScoreDisplay.SetActive(true);
    }
}