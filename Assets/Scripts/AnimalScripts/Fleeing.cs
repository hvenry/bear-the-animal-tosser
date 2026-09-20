/// <summary>
/// The animal runs directly away from the player, re-checking on a fixed interval
/// whether it has gained enough distance to calm down.
/// </summary>
public class Fleeing : AnimalBaseState
{
    // How often the "am I far enough away yet?" check runs, in seconds.
    private const float CheckInterval = 2f;

    public Fleeing(MovementSM stateMachine) : base("Flee", stateMachine) { }

    public override void UpdateLogic()
    {
        MoveAlong(AwayFromPlayer);

        if (sm.timeSpent % CheckInterval < 0.1f && !PlayerIsNear)
        {
            stateMachine.ChangeState(sm.idleState);
        }
    }
}
