using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : BaseState
{
    //reference to current objects state machine 
    private MovementSM _sm;
    
    // player 
    public Transform target;
    
    // distance which the animal starts to flee from player 
    public float threshold = 25f;
    public Vector2 dir;
    public Roaming(MovementSM stateMachine) : base("Roam", stateMachine) {
        _sm = stateMachine;
        target = GameObject.FindWithTag("Player").transform;

    }

    public override void Enter()
    {
        dir = GetRandDir();
    }
    //
    // public override void Exit()
    // {
    //     _sm.timer.Stop();
    // }

    public override void UpdateLogic()
    {
        // checks the distance between itself and the player
        // if within threshold range, change to flee state
        Vector2 direction = _sm.rigidbody.position - (Vector2)target.position;
        if (direction.sqrMagnitude < threshold)
        {
            stateMachine.ChangeState(_sm.fleeState);
        }
        else if (_sm.timeSpent > _sm.roamTimer)
        {
            stateMachine.ChangeState(_sm.idleState);
        }
        _sm.rigidbody.MovePosition(_sm.rigidbody.position + dir.normalized * Time.deltaTime * _sm.speed);

    }

    private Vector2 GetRandDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
    }
}