using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrown : BaseState
{
    // the position of the animal when it is thrown 
    public Vector3 start;
    
    // the position of the mouse in the game world 
    private Vector3 worldPosition;
    
    // time passed since it has been thrown 
    protected float timePassed;
    
    //state machine
    private MovementSM _sm;
    
    //player
    public Transform target;
    public Thrown(MovementSM stateMachine) : base("Thrown", stateMachine) {
        _sm = stateMachine;
        target = GameObject.FindWithTag("Player").transform;
    }

    public override void Enter()
    {
        // find the position of the mouse in the world 
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // make sure the animal lands on top the rest of the map 
        worldPosition += new Vector3(0, 0, 5);
        
        //start at the player's position 
        // can change to the hold spot itself so it doesn't spawn in the player
        start = target.position;
        timePassed = 0f;
        
        // do not want collision during the flight, makes the player not bounce back when throwing the animal
        _sm.collider2D.enabled = false;


    }

    public override void UpdateLogic()
    {
        // if the flight is done 
        if (timePassed > 2f)
        {
            stateMachine.ChangeState(_sm.idleState);
        }
        
        
        timePassed += Time.deltaTime;
        // instead of doing constant math, travels along a preset path over the span of 2 seconds
        _sm.rigidbody.transform.position = MathParabola.Parabola(start, worldPosition, 2f, timePassed / 2f);

        // every x seconds go roam 
        //TBC 
    }
    public override void Exit()
    {
        // reenable collisions after landing 
        _sm.collider2D.enabled = true;
    }
}