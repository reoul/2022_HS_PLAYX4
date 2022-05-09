using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State _curState;
    
    /// <summary>
    /// key에 있는 int는 각 StateMachine마다 Enum을 줘서 int로 형변환 후 사용
    /// </summary>
    public Dictionary<int, State> StateDictionary;
    
    /// <summary>
    /// 상태를 딕셔너리에 추가해준다
    /// </summary>
    public abstract void InitStateDictionary();

    public void ChangeState(State state)
    {
        if (_curState == state)
        {
            Debug.Log($"{gameObject.name}의 현재 상태는 {_curState.ToString()} 새로운 상태는{state.ToString()}");
            return;
        }

        if (_curState != null)
        {
            Debug.Log($"{gameObject.name}의 현재 상태는 {_curState.ToString()} 새로운 상태는{state.ToString()}");
            _curState.StateExit();
        }

        _curState = state;
        _curState.StateStart();
    }

    public void StateUpdate()
    {
        _curState.StateUpdate();
    }
}
