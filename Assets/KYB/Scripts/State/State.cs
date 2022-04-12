using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected readonly StateMachine _stateMachine;

    protected State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public virtual void StateStart(){}
    public abstract void StateUpdate();
    public virtual void StateExit(){}
}
