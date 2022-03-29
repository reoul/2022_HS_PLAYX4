using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    //Enemy 초기화
    public enum EnemyType { None, stage1, stage2, stage3 };

    public string name { get; private set; }
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    public Enemy()
    {
    }

    //전략
    private enum EnemyState { Spawn, Idle, Run, Attack, Hit, Death };
    private StateMachine _stateMachine;
    private Dictionary<EnemyState, IState> _dicState;

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

    public class StateSpawn : IState
    {
        private GameObject gameObject;
        public StateSpawn(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public void Enter()
        {
            gameObject.GetComponent<BeamController>().BeamIn();
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (gameObject.transform.GetChild(1).GetComponent<Renderer>().material.GetFloat("_DisintegrateAmount") == 0)
            {
                gameObject.GetComponent<Enemy>()._stateMachine.SetState(gameObject.GetComponent<Enemy>()._dicState[EnemyState.Run]);
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
            gameObject.GetComponent<Animator>().Play("Walk");
        }

        public void Exit()
        {

        }

        public void Update()
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _targetTransform.position, 0.01f);
            float _distance = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z), new Vector2(_targetTransform.position.x, _targetTransform.position.z));
            if (_distance < 1.5f)
            {
                gameObject.GetComponent<Enemy>()._stateMachine.SetState(gameObject.GetComponent<Enemy>()._dicState[EnemyState.Attack]);
                Debug.Log(_distance);
            }
        }
    }
    public class StateAttack : IState
    {
        private GameObject gameObject;
        private Transform _targetTransform;
        public StateAttack(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public void Enter()
        {
            _targetTransform = GameObject.Find("[CameraRig]").transform;
            gameObject.GetComponent<Animator>().Play("Attack");
        }

        public void Exit()
        {

        }

        public void Update()
        {

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
        private GameObject gameObject;
        public StateDeath(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public void Enter()
        {
            gameObject.GetComponent<BeamController>().BeamOut(true);
        }

        public void Exit()
        {

        }

        public void Update()
        {

        }
    }
    //피격 판정
    override public void Damage(float damage)
    {
        OnDamage();
    }

    private void OnDamage() { }


    //UnityEngine
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        InitState();
    }

    private void Update()
    {
        _stateMachine.DoUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        _stateMachine.SetState(_dicState[EnemyState.Death]);
    }
}

