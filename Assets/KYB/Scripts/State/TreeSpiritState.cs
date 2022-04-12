using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpiritState : StateMachine
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
        //InitStateDictionary();
    }

    public override void InitStateDictionary()
    {
        StateDictionary = new Dictionary<int, State>();
        StateDictionary.Add((int) StateType.Idle, new IdleState(gameObject));
        StateDictionary.Add((int) StateType.Spawn, new SpawnState(gameObject));
        StateDictionary.Add((int) StateType.Run, new RunState(gameObject));
        StateDictionary.Add((int) StateType.Hit, new HitState(gameObject));
        StateDictionary.Add((int) StateType.Death, new DeathState(gameObject));
        ChangeState(StateDictionary[(int) StateType.Idle]);
    }

    private class IdleState : State
    {
        public IdleState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            base.StateStart();
            //var transform = gameObject.transform;
            //Debug.Log(_stateMachine.gameObject.transform.position);
            //transform.forward = Camera.main.gameObject.transform.position - transform.position;
        }

        public override void StateUpdate()
        {
        }
    }

    private class SpawnState : State
    {
        public SpawnState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateStart()
        {
            base.StateStart();
            Debug.Log("스폰");
        }

        public override void StateUpdate()
        {
            if (gameObject.GetComponentInChildren<DissolveMat>().Percent >= 1)
            {
                Debug.Log("다됨");
                //var aaaa = gameObject.GetComponent<StateMachine>();
                var staticMachine = gameObject.GetComponent<TreeSpirit>()._stateMachine;
                staticMachine.ChangeState(staticMachine.StateDictionary[(int)StateType.Run]);
                //gameObject.GetComponent<StateMachine>().ChangeState(_stateMachine.StateDictionary[(int)StateType.Run]);
            }
        }
    }

    private class RunState : State
    {
        private readonly Transform _treeSpiritTransform;

        public RunState(GameObject gameObject) : base(gameObject)
        {
            _treeSpiritTransform = gameObject.transform;
        }

        public override void StateUpdate()
        {
            float moveSpeed = gameObject.GetComponent<TreeSpirit>().MoveSpeed;
            _treeSpiritTransform.Translate(-_treeSpiritTransform.forward * moveSpeed * Time.deltaTime);

            var distance = Vector3.Distance(_treeSpiritTransform.position, Vector3.zero);
            if (distance < 0.5f)
            {
                _stateMachine.ChangeState(_stateMachine.StateDictionary[(int) StateType.Death]);
            }
        }
    }

    private class HitState : State
    {
        public HitState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateUpdate()
        {
            if (gameObject.GetComponentInChildren<DissolveMat>().Percent <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private class DeathState : State
    {
        public DeathState(GameObject gameObject) : base(gameObject)
        {
        }

        public override void StateUpdate()
        {
            if (gameObject.GetComponentInChildren<DissolveMat>().Percent <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}