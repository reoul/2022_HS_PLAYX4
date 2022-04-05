using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    protected GameObject _gameObj;
    protected StateMachine _stateMachine { get { return _gameObj.GetComponent<Strategy>()._stateMachine; }}
    protected Dictionary<Strategy.State, IState> _dicState;

    public IState(GameObject gameObject)
    {
        _gameObj = gameObject;
        _dicState = _gameObj.GetComponent<Strategy>()._dicState;
    }
    
    public abstract void StateEnter();
    public abstract void StateUpdate();
    public abstract void StateExit();
}

public class StateMachine
{
    public IState CurrentState { get; private set; }

    public StateMachine(IState defaultState)
    {
        CurrentState = defaultState;
        CurrentState.StateEnter();
    }
    public void SetState(IState state)
    {
        if(CurrentState == state){
            return;
        }
        CurrentState.StateExit();
        CurrentState = state;
        CurrentState.StateEnter();
    }

    public void DoUpdate()
    {
        CurrentState.StateUpdate();
    }
}
