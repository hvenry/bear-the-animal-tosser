/// <summary>
/// The animal is in the bear's arms. Collision is switched off so the carried
/// animal does not shove the player around; PickUp drives its position.
/// </summary>
public class Held : AnimalBaseState
{
    public Held(MovementSM stateMachine) : base("Held", stateMachine) { }

    public override void Enter()
    {
        sm.collider2D.enabled = false;
    }

    public override void Exit()
    {
        sm.collider2D.enabled = true;
    }
}
