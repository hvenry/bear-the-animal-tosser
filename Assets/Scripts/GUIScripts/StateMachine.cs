using UnityEngine;

/// <summary>
/// Minimal state machine. Tracks the current state, forwards the Unity update
/// hooks to it, and measures how long the object has been in that state.
/// </summary>
public class StateMachine : MonoBehaviour
{
    // The state this object is currently in.
    public BaseState currentState;

    // Seconds spent in the current state; reset on every transition.
    public float timeSpent;

    private void Start()
    {
        currentState = GetInitialState();
        timeSpent = 0f;
        currentState?.Enter();
    }

    private void Update()
    {
        timeSpent += Time.deltaTime;
        currentState?.UpdateLogic();
    }

    private void LateUpdate()
    {
        currentState?.UpdatePhysics();
    }

    public void ChangeState(BaseState newState)
    {
        if (newState == null)
        {
            return;
        }

        currentState?.Exit();
        timeSpent = 0f;
        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// The state an object starts in. Subclasses override this; see MovementSM.
    /// </summary>
    protected virtual BaseState GetInitialState() => null;
}
