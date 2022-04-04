using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategySpider : MonoBehaviour
{
    //전략

    enum EnemyState { Spawn, Idle, Run, Attack, Hit, Death };
    StateMachine _stateMachine;
    Dictionary<EnemyState, IState> _dicState;

    private void InitState()
    {
        _dicState = new Dictionary<EnemyState, IState>();

        IState spawn = new StateSpawn(this.gameObject);
        IState idle = new StateIdle(this.gameObject);
        IState run = new StateRun(this.gameObject);
        IState attack = new StateAttack(this.gameObject);
        IState hit = new StateHit(this.gameObject);
        IState death = new StateDeath(this.gameObject);

        _dicState.Add(EnemyState.Spawn, spawn);
        _dicState.Add(EnemyState.Idle, idle);
        _dicState.Add(EnemyState.Run, run);
        _dicState.Add(EnemyState.Attack, attack);
        _dicState.Add(EnemyState.Hit, hit);
        _dicState.Add(EnemyState.Death, death);

        _stateMachine = new StateMachine(spawn);
    }

    private void OnEnable()
    {
        InitState();
    }


    private void Update()
    {
        _stateMachine.DoUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "PlayerZone")
        {
            _stateMachine.SetState(_dicState[EnemyState.Attack]);
        }
        else if(other.tag == "Arrow")
        {
            _stateMachine.SetState(_dicState[EnemyState.Death]);
        }
    }

    //States
    public class StateSpawn : IState
    {
        public GameObject gameObj;

        public StateSpawn(GameObject gameObject)
        {
            this.gameObj = gameObject;
        }

        private disolveSpider _disolveSpider;

        public void Enter()
        {
            _disolveSpider = gameObj.GetComponentInChildren<disolveSpider>();
            _disolveSpider.InitTime();
        }

        public void Exit()
        {

        }

        public void Update()
        {

            //if (gameObject.transform.GetChild(1).GetComponent<Renderer>().material.GetFloat("_DisintegrateAmount") <= 0)
            //{
            //    gameObject.GetComponent<StrategySpider>()._stateMachine.SetState(gameObject.GetComponent<StrategySpider>()._dicState[EnemyState.Run]);
            //}
            //else if (gameObject.transform.GetChild(1).GetComponent<Renderer>().material.GetFloat("_DisintegrateAmount") >= 1)
            //{
            //    gameObject.GetComponent<BeamController>().BeamIn();
            //}
            if(_disolveSpider.Disolve())
            {
                gameObj.GetComponent<StrategySpider>()._stateMachine.SetState(gameObj.GetComponent<StrategySpider>()._dicState[EnemyState.Run]);
            }
        }
    }
    public class StateIdle : IState
    {
        private GameObject gameObject;
        public StateIdle(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
    public class StateRun : IState
    {
        private GameObject gameObject;
        private Transform _targetTransform;
        public StateRun(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public void Enter()
        {
            _targetTransform = GameObject.Find("[CameraRig]").transform;
            gameObject.GetComponent<Animator>().Play("Run");
        }

        public void Exit()
        {

        }

        public void Update()
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(_targetTransform.position.x, gameObject.transform.position.y, _targetTransform.position.z), 0.06f);
            float _distance = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z), new Vector2(_targetTransform.position.x, _targetTransform.position.z));
            if (_distance < 7)
            {
                gameObject.GetComponent<StrategySpider>()._stateMachine.SetState(gameObject.GetComponent<StrategySpider>()._dicState[EnemyState.Attack]);
            }
        }
    }
    public class StateAttack : IState
    {
        private GameObject gameObj;
        public StateAttack(GameObject gameObject)
        {
            this.gameObj = gameObject;
        }

        private Transform _targetTransform;

        private disolveSpider _disolveSpider;

        public void Enter()
        {
            _disolveSpider = gameObj.GetComponentInChildren<disolveSpider>();
            _targetTransform = GameObject.Find("[CameraRig]").transform;
            _disolveSpider.InitTime();
            ScoreSystem.score -= 100;
            _disolveSpider.InitAA();
        }

        public void Exit()
        {

        }

        public void Update()
        {
            gameObj.transform.position = Vector3.MoveTowards(gameObj.transform.position, new Vector3(_targetTransform.position.x, gameObj.transform.position.y, _targetTransform.position.z), 0.06f);
            if(_disolveSpider.DeleteDisolve())
            {
                gameObj.SetActive(false);
            }
        }
    }
    public class StateHit : IState
    {
        private GameObject gameObject;
        public StateHit(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
    public class StateDeath : IState
    {
        private GameObject gameObj;
        private Transform _targetTransform;
        public StateDeath(GameObject gameObject)
        {
            this.gameObj = gameObject;
            _targetTransform = GameObject.Find("[CameraRig]").transform;
        }

        private disolveSpider _disolveSpider;

        public void Enter()
        {
            _disolveSpider = gameObj.GetComponentInChildren<disolveSpider>();
            _disolveSpider.InitTime();
            ScoreSystem.score += 100;
            _disolveSpider.InitAA();
        }

        public void Exit()
        {

        }

        public void Update()
        {
            gameObj.transform.position = Vector3.MoveTowards(gameObj.transform.position, new Vector3(_targetTransform.position.x, gameObj.transform.position.y, _targetTransform.position.z), 0.06f);
            if(_disolveSpider.DeleteDisolve())
            {
                gameObj.SetActive(false);
            }
        }
    }

}
