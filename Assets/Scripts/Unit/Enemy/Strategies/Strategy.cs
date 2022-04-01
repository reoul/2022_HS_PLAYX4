using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Strategy : MonoBehaviour
{
    public enum State { Spawn, Idle, Run, Attack, Hit, Death };
    public StateMachine _stateMachine { get; set; }
    public Dictionary<State, IState> _dicState { get; protected set; }

    public virtual float MoveToPlayer(float speed) { return 0; }
}
