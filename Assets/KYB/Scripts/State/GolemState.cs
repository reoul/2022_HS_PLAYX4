using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GolemState : StateMachine
{
    public enum StateType
    {
        Idle = 0,
        Spawn,
        JumpAttack,
        SwingAttack
    }

    /// <summary>
    /// 보스 처음 위치
    /// </summary>
    public Vector3 BasePos { get; private set; }

    /// <summary>
    /// 보스 이동 위치
    /// </summary>
    public Vector3 DestPos { get; private set; }

    public bool CanMove { get; private set; }

    public bool IsMoveForward { get; private set; }

    private void Awake()
    {
        InitStateDictionary();
    }

    private void Start()
    {
        BasePos = transform.position;
    }

    public override void InitStateDictionary()
    {
        StateDictionary = new Dictionary<int, State>();
        StateDictionary.Add((int) StateType.Idle, new IdleState(gameObject));
        StateDictionary.Add((int) StateType.Spawn, new SpawnState(gameObject));
        StateDictionary.Add((int) StateType.JumpAttack, new JumpAttackState(gameObject));
        StateDictionary.Add((int) StateType.SwingAttack, new SwingAttackState(gameObject));
        ChangeState(StateDictionary[(int) StateType.Idle]);
    }

    public void MoveToTarget1()
    {
        CanMove = true;
        IsMoveForward = true;
    }

    public void SetTargetZero1()
    {
        IsMoveForward = false;
        CanMove = true;
    }

    private class IdleState : State
    {
        private float _time;

        public IdleState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _time = 0;
        }

        public override void StateUpdate()
        {
            _time += Time.deltaTime;
            if (_time >= 5)
            {
                int rand = Random.Range(0, 2);
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int) StateType.JumpAttack + rand]);
            }
        }
    }

    private class SpawnState : State
    {
        public SpawnState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateUpdate()
        {
        }
    }

    private class JumpAttackState : State
    {
        private float _time = 0;
        private GolemState _golemState;

        public JumpAttackState(GameObject gameObject) : base(gameObject)
        {
            _golemState = gameObject.GetComponent<GolemState>();
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("JumpAttack");
        }

        public override void StateUpdate()
        {
            if (_golemState.CanMove)
            {
                _time += (_golemState.IsMoveForward ? 1 : -1.5f) * Time.deltaTime;
                Debug.Log(_time);
                _gameObject.transform.position = Vector3.Lerp(_golemState.BasePos, _golemState.DestPos, _time);
            }
        }
    }

    private class SwingAttackState : State
    {
        public SwingAttackState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            _gameObject.GetComponent<Animator>().SetTrigger("SwingAttack");
        }

        public override void StateUpdate()
        {
        }
    }
}