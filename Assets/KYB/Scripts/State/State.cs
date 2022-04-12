using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected KYB_StateMachine _stateMachine;

    protected State(KYB_StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    
    public virtual void StateStart(){}
    public abstract void StateUpdate();
    public virtual void StateExit(){}
}
