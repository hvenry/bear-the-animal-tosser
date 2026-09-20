using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Idle script is the default state of the animals
public class Idle : BaseState
{
    // We need to access the animal's state machine to change states
    private MovementSM _sm;

    // We need to access the player's position to check if the animal is close enough to flee
    public Transform target;
    
    // If the distance is less than a specified threshold value, the animal will change state
    // to the Flee state
    public float threshold = 25f;
    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) {
        _sm = stateMachine;
        target = GameObject.FindWithTag("Player").transform;

    }

    // Enter() is called when the state machine enters the state associated with the Idle script,
    // using this we can possibly play an idle animation on the animal, or play a sound when its
    // idle (not necessary but we could do it)
    // public override void Enter()
    // {
    //     //_sm.timer.Set(3f);
    // }
    //
    // public override void Exit()
    // {
    //     // _sm.timer.Stop();
    // }

    // This method is defined in the BaseState class, and is called every frame of the game loop,
    // while the state machine is in the state associated with the Idle script (Idle state)
    public override void UpdateLogic()
    {
        // This is the direction vector from the animal to the player
        Vector2 direction = _sm.rigidbody.position - (Vector2)target.position;

        // change state to flee if the distance is less than the threshold
        if (direction.sqrMagnitude < threshold)
        {
            stateMachine.ChangeState(_sm.fleeState);
        }
        else if (_sm.timeSpent > _sm.roamTimer)
        {
            stateMachine.ChangeState(_sm.roamState);
        }
        
    }
    
}