using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleeing : BaseState
{
    private MovementSM _sm;
    
    // player reference 
    private Transform _target;
    
    //time spent in flee
    private float _timeInterval = 2f;
    
    //radius around player to switch back into Idle
    private float _threshold = 25f;
    public Fleeing(MovementSM stateMachine) : base("Flee", stateMachine) {
        _sm = stateMachine;
        _target = GameObject.FindWithTag("Player").transform;
    }

    // public override void Enter()
    // {
    //     _sm.timer.Set(3f);
    // }
    //
    // public override void Exit()
    // {
    //     _sm.timer.Stop();
    // }

    public override void UpdateLogic()
    {
        // direction points away from player
        Vector2 direction = _sm.rigidbody.position - (Vector2)_target.position;
        
        // move animal away from player
        _sm.rigidbody.MovePosition(_sm.rigidbody.position + direction.normalized * Time.deltaTime * _sm.speed);
        
        if (_sm.timeSpent % _timeInterval < 0.1f)
        {
            // Debug.Log("inside of timer");

            if (direction.sqrMagnitude > _threshold)
            {
                // Debug.Log("Change out of fleeing");
                stateMachine.ChangeState(_sm.idleState);
            } 
        }
    }
}