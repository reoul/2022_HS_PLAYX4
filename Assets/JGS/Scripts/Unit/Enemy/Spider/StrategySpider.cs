using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategySpider : Strategy
{
    private Transform _target;

    //전략
    private void InitState()
    {
        _dicState = new Dictionary<State, IState>();
        IState spawn = new StateSpawn(this.gameObject);
        IState idle = new StateIdle(this.gameObject);
        IState run = new StateRun(this.gameObject);
        IState attack = new StateAttack(this.gameObject);
        IState hit = new StateHit(this.gameObject);
        IState death = new StateDeath(this.gameObject);

        _dicState.Add(State.Spawn, spawn);
        _dicState.Add(State.Idle, idle);
        _dicState.Add(State.Run, run);
        _dicState.Add(State.Attack, attack);
        _dicState.Add(State.Hit, hit);
        _dicState.Add(State.Death, death);

        _stateMachine = new StateMachine(spawn);
    }

    private void OnEnable()
    {
        _target = GameObject.Find("[CameraRig]").transform;
        InitState();
    }


    private void Update()
    {
        _stateMachine.DoUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_stateMachine.CurrentState == _dicState[State.Run])
        {
            if(other.name == "PlayerZone")
            {
                _stateMachine.SetState(_dicState[State.Attack]);
            }
            else if(other.tag == "Arrow")
            {
                _stateMachine.SetState(_dicState[State.Death]);
            }  
        }
    }

    public override float MoveToPlayer(float speed)
    {
        this.GetComponent<AiSpider>().SetTarget(_target);
        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(_targetPostion.x, gameObject.transform.position.y, _targetPostion.z), speed * Time.deltaTime * 120);
        float _distance = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z), new Vector2(_target.position.x, _target.position.z));

        return _distance;
    }

    //States
    private class StateSpawn : IState
    {
        public StateSpawn(GameObject gameObject) : base(gameObject) { }

        private KYB_Dissolve _disolve;

        public override void StateEnter()
        {
            _disolve = _gameObj.GetComponentInChildren<KYB_Dissolve>();
            _disolve.StartCreateDissolve();
        }

        public override void StateExit()
        {

        }

        public override void StateUpdate()
        {

            //if (gameObject.transform.GetChild(1).GetComponent<Renderer>().material.GetFloat("_DisintegrateAmount") <= 0)
            //{
            //    gameObject.GetComponent<StrategySpider>()._stateMachine.SetState(gameObject.GetComponent<StrategySpider>()._dicState[State.Run]);
            //}
            //else if (gameObject.transform.GetChild(1).GetComponent<Renderer>().material.GetFloat("_DisintegrateAmount") >= 1)
            //{
            //    gameObject.GetComponent<BeamController>().BeamIn();
            //}
            if (_disolve.State == KYB_Dissolve.DissolveState.Nomal)
            {
                _stateMachine.SetState(_dicState[State.Run]);
            }
        }
    }
    private class StateIdle : IState
    {
        public StateIdle( GameObject gameObject) : base( gameObject) { }
        public override void StateEnter()
        {

        }

        public override void StateExit()
        {

        }

        public override void StateUpdate()
        {

        }
    }
    private class StateRun : IState
    {
        public StateRun( GameObject gameObject) : base( gameObject) { }


        public override void StateEnter()
        {
            _gameObj.GetComponent<Animator>().Play("Run");
        }

        public override void StateExit()
        {

        }

        public override void StateUpdate()
        {
            if (_gameObj.GetComponent<Strategy>().MoveToPlayer(0.06f) < 7)
            {
                _stateMachine.SetState(_dicState[State.Attack]);
            }
        }
    }
    private class StateAttack : IState
    {
        public StateAttack( GameObject gameObject) : base( gameObject) { }

        private Transform _targetTransform;

        private KYB_Dissolve _disolve;

        public override void StateEnter()
        {
            _disolve = _gameObj.GetComponentInChildren<KYB_Dissolve>();
            ScoreSystem.score -= 100;
            _disolve.StartDestroyDissolve();
        }

        public override void StateExit()
        {

        }

        public override void StateUpdate()
        {
            _gameObj.GetComponent<Strategy>().MoveToPlayer(0.06f);
            if (_disolve.State == KYB_Dissolve.DissolveState.Hide)
            {
                _gameObj.GetComponent<AiSpider>().DisableTarget();
                _gameObj.SetActive(false);
            }
        }
    }
    private class StateHit : IState
    {
        private GameObject gameObject;
        public StateHit( GameObject gameObject) : base( gameObject) { }

        public override void StateEnter()
        {

        }

        public override void StateExit()
        {

        }

        public override void StateUpdate()
        {

        }
    }
    private class StateDeath : IState
    {
        private Transform _targetTransform;
        public StateDeath( GameObject gameObject) : base( gameObject) { }

        private KYB_Dissolve _disolve;

        public override void StateEnter()
        {
            _disolve = _gameObj.GetComponentInChildren<KYB_Dissolve>();
            ScoreSystem.score += 100;
            _disolve.StartDestroyDissolve();
        }

        public override void StateExit()
        {

        }

        public override void StateUpdate()
        {
            _gameObj.GetComponent<Strategy>().MoveToPlayer(0.06f);
            if (_disolve.State == KYB_Dissolve.DissolveState.Hide)
            {
                _gameObj.SetActive(false);
            }
        }
    }

}
