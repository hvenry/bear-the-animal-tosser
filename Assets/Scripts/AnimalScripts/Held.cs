using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Held : BaseState
{
    
    private MovementSM _sm;
    public Transform target;
    public Held(MovementSM stateMachine) : base("Held", stateMachine) {
        _sm = stateMachine;
        target = GameObject.FindWithTag("Player").transform;
    }

    public override void Enter()
    {
        // no collision physics if held
        _sm.collider2D.enabled = false;
    }

    public override void UpdateLogic()
    {
        
    }


}