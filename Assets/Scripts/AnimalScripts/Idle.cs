/// <summary>
/// Default state. The animal stands still, watching for the player to come close
/// (flee) or for its roam timer to elapse (wander off).
/// </summary>
public class Idle : AnimalBaseState
{
    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) { }

    public override void UpdateLogic()
    {
        if (PlayerIsNear)
        {
            stateMachine.ChangeState(sm.fleeState);
        }
        else if (sm.timeSpent > sm.roamTimer)
        {
            stateMachine.ChangeState(sm.roamState);
        }
    }
}
