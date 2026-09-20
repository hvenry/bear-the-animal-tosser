using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    // Access all our animal states (scripts) inside one script
    //[HideInInspector]
    public Idle idleState;
    
    //[HideInInspector]
    public Roaming roamState;
    
    //[HideInInspector]
    public Fleeing fleeState;
    
    //[HideInInspector]
    public Held heldState;
    
    //[HideInInspector]
    public Thrown thrownState;
    
    [HideInInspector]
    public Rigidbody2D rigidbody;

    [HideInInspector]
    public BoxCollider2D collider2D;

    public int id = new int();

    // Hide HideInInspector makes the variables only accessable in the script
    // [HideInInspector]
    public float speed = 10f;

    public Timer timer;
    public float roamTimer;
    public int weight;


    // Awake always gets called the first time a game object is created in a scene
    private void Awake()
    {
        // For our movement, we will need to access all animal states inside one script
        // and add them to the animal's state machine
        roamTimer = Random.Range(1.5f, 3f);
        idleState = new Idle(this);
        roamState = new Roaming(this);
        fleeState = new Fleeing(this);
        heldState = new Held(this);
        thrownState = new Thrown(this);
        rigidbody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        timer = gameObject.AddComponent<Timer>();
    }

    // Override the GetInitialState method to return the initial state of the animal (idleState)
    // Overrides the fnc in StateMachine
    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
