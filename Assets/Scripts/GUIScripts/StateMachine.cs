using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // reference to the objects current state
    public BaseState currentState;

    public float timeSpent;
    // Start is called before the first frame update
    void Start()
    {
        // initial state for animals is idle 
        currentState = GetInitialState();
        timeSpent = 0f;
        // null propagation, will not call if null
        currentState?.Enter();

    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime;
        // null propagation, will not call if null
        currentState?.UpdateLogic();
    }


    void LateUpdate()
    {
        // null propagation, will not call if null
        currentState?.UpdatePhysics();
    }

    public void ChangeState(BaseState newState)
    {
        // on change of state, call the outgoing state's exit fnc
        currentState.Exit();

        timeSpent = 0f;

        currentState = newState;
        // call new states enter function 
        currentState.Enter();
    }
    private void OnGUI()
    // will be removed, from tutorial code 
    {
        // if ((currentState.name == "Roam")|(currentState.name == "Thrown"))
        // {
        //     string content = currentState != null ? currentState.name : "(NA)";
        //     GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        // }
        
    }

    protected virtual BaseState GetInitialState()
    // default function, gets overriden and initial state for animals is idle
    // see MovementSM fnc
    {
        return null;
    }
}