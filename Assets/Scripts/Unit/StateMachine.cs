using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

public class StateMachine
{
    public IState CurrentState { get; private set; }

    public StateMachine(IState defaultState)
    {
        CurrentState = defaultState;
        CurrentState.Enter();
    }
    public void SetState(IState state)
    {
        if(CurrentState == state){
            return;
        }
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }

    public void DoUpdate()
    {
        CurrentState.Update();
    }
}
