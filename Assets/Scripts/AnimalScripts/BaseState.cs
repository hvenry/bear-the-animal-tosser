/// <summary>
/// Base class for every state an animal can be in. The owning
/// <see cref="StateMachine"/> calls these hooks; states override what they need.
/// </summary>
public class BaseState
{
    public string name;

    // The state machine that owns this state instance.
    protected StateMachine stateMachine;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}
