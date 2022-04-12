using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine { get { return gameObject.GetComponent<StateMachine>(); } }

    protected readonly GameObject gameObject;

    protected State(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    
    public virtual void StateStart(){}
    public abstract void StateUpdate();
    public virtual void StateExit(){}
}
