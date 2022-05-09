using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitObj : MonoBehaviour, IHitable
{
    public void HitEvent()
    {
        // todo : 게임 종료 되는지 확인
        Application.Quit();
    }
}
