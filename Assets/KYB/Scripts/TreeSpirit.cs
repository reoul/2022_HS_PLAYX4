using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeSpirit : MonoBehaviour
{
    public Transform[] weakPoints;

    [SerializeField] private float _moveSpeed;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public StateMachine _stateMachine;

    private Transform _curWeak;

    public void Awake()
    {
        //RandomWeak();
    }

    public void Init()
    {
        _stateMachine = new TreeSpiritState(gameObject);
    }

    private void Update()
    {
        _stateMachine.StateUpdate();
    }

    public void ChangeState(TreeSpiritState.StateType state)
    {
        _stateMachine.ChangeState(_stateMachine.StateDictionary[(int)state]);
    }

    private void RandomWeak()
    {
        _curWeak = weakPoints[Random.Range(0, weakPoints.Length)];
        _curWeak.gameObject.SetActive(true);
    }

    /// <summary>
    /// 약점을 변경한다
    /// </summary>
    public void ChangeWeakPoint()
    {
        int rand;
        do
        {
            rand = Random.Range(0, weakPoints.Length);
        } while (_curWeak == weakPoints[rand]);
        _curWeak.gameObject.SetActive(false);
        weakPoints[rand].gameObject.SetActive(true);
        _curWeak = weakPoints[rand];
    }
}