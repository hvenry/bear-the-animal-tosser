using UnityEngine;

/// <summary>
/// The animal wanders in one random direction until the player spooks it or its
/// roam timer elapses and it settles back into Idle.
/// </summary>
public class Roaming : AnimalBaseState
{
    private Vector2 _direction;

    public Roaming(MovementSM stateMachine) : base("Roam", stateMachine) { }

    public override void Enter()
    {
        // A fresh heading each time the animal starts roaming.
        _direction = Random.insideUnitCircle.normalized;
    }

    public override void UpdateLogic()
    {
        if (PlayerIsNear)
        {
            stateMachine.ChangeState(sm.fleeState);
            return;
        }

        if (sm.timeSpent > sm.roamTimer)
        {
            stateMachine.ChangeState(sm.idleState);
            return;
        }

        MoveAlong(_direction);
    }
}
