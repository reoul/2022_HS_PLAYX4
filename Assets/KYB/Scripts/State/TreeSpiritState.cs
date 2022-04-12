using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpiritState : KYB_StateMachine
{
    public enum StateType
    {
        Idle,
        Spawn,
        Run,
        Hit,
        Death
    }

    public TreeSpiritState(GameObject gameObject) : base(gameObject)
    {
    }

    protected override void InitStateDictionary()
    {
        StateDictionary = new Dictionary<int, State>();
        StateDictionary.Add((int) StateType.Idle, new IdleState(this));
        StateDictionary.Add((int) StateType.Spawn, new SpawnState(this));
        StateDictionary.Add((int) StateType.Run, new RunState(this));
        StateDictionary.Add((int) StateType.Hit, new HitState(this));
        StateDictionary.Add((int) StateType.Death, new DeathState(this));
        ChangeState(StateDictionary[(int) StateType.Idle]);
    }

    private class IdleState : State
    {
        public IdleState(KYB_StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void StateStart()
        {
            base.StateStart();
            var transform = _stateMachine.gameObject.transform;
            transform.forward = new Vector3(0, transform.position.y, 0) - transform.position;
        }

        public override void StateUpdate()
        {
        }
    }

    private class SpawnState : State
    {
        public SpawnState(KYB_StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void StateUpdate()
        {
            if (_stateMachine.gameObject.GetComponentInChildren<DissolveMat>().Percent >= 1)
            {
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int) StateType.Run]);
            }
        }
    }

    private class RunState : State
    {
        private readonly Transform _treeSpiritTransform;

        public RunState(KYB_StateMachine stateMachine) : base(stateMachine)
        {
            _treeSpiritTransform = stateMachine.gameObject.transform;
        }

        public override void StateUpdate()
        {
            float moveSpeed = _stateMachine.gameObject.GetComponent<TreeSpirit>().MoveSpeed;
            _treeSpiritTransform.Translate(_treeSpiritTransform.forward * moveSpeed * Time.deltaTime);
            
            var distance = Vector3.Distance(_treeSpiritTransform.position, Vector3.zero);
            if (distance < 5)
            {
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int) StateType.Death]);
            }
        }
    }

    private class HitState : State
    {
        public HitState(KYB_StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void StateUpdate()
        {
            if (_stateMachine.gameObject.GetComponentInChildren<DissolveMat>().Percent <= 0)
            {
                _stateMachine.gameObject.SetActive(false);
            }
        }
    }

    private class DeathState : State
    {
        public DeathState(KYB_StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void StateUpdate()
        {
            if (_stateMachine.gameObject.GetComponentInChildren<DissolveMat>().Percent <= 0)
            {
                _stateMachine.gameObject.SetActive(false);
            }
        }
    }
}