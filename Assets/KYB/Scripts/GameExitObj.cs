using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitObj : MonoBehaviour, IHitable
{
    public void HitEvent()
    {
        Application.Quit();
    }
}
