using System;
using UnityEngine;

[RequireComponent(typeof(WendigoState))]
public class Wendigo : Enemy, IHitable
{
    private void Awake()
    {
        _stateMachine = this.GetComponent<StateMachine>();
    }
    
    private void Update()
    {
        _stateMachine.StateUpdate();
    }

    public void ChangeState(WendigoState.StateType state)
    {
        _stateMachine.ChangeState(_stateMachine.StateDictionary[(int)state]);
    }

    public void HitEvent()
    {
        ScoreSystem.Score += 100;
        ChangeState(WendigoState.StateType.Hit);
        EffectManager.Instance.CreateEffect(this.transform.position + new Vector3(0, transform.lossyScale.y * 1.7f,0));
        FindObjectOfType<EnemySpawner>().Delete(this.gameObject);
        SoundManager.Instance.PlaySoundFourth("Wendigo_Dead", 1f);
    }
}