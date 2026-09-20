using UnityEngine;

/// <summary>
/// The animal is in flight toward where the player clicked. It follows a fixed
/// parabolic arc over <see cref="FlightDuration"/> rather than using physics, so
/// the throw always lands exactly where the player aimed.
/// </summary>
public class Thrown : AnimalBaseState
{
    // Seconds the arc takes from the player's hands to the landing spot.
    private const float FlightDuration = 2f;

    // Height of the arc, in world units.
    private const float ArcHeight = 2f;

    private Vector3 _start;
    private Vector3 _landing;
    private float _timePassed;

    public Thrown(MovementSM stateMachine) : base("Thrown", stateMachine) { }

    public override void Enter()
    {
        // Falls back to the animal's own position if the player or camera is
        // missing, so a throw can never dereference null mid-state-change.
        _start = HasTarget ? Target.position : sm.rigidbody.transform.position;

        Camera camera = Camera.main;
        _landing = camera != null
            ? camera.ScreenToWorldPoint(Input.mousePosition)
            : _start;

        // Push the landing point behind the tilemap so the animal draws on top of
        // the ground rather than inside it.
        _landing.z = _start.z + 5f;

        _timePassed = 0f;

        // No collisions mid-flight, otherwise the throw shoves the player back.
        sm.collider2D.enabled = false;
    }

    public override void UpdateLogic()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed >= FlightDuration)
        {
            // Snap to the exact landing spot before handing control back.
            sm.rigidbody.transform.position = _landing;
            stateMachine.ChangeState(sm.idleState);
            return;
        }

        sm.rigidbody.transform.position =
            MathParabola.Parabola(_start, _landing, ArcHeight, _timePassed / FlightDuration);
    }

    public override void Exit()
    {
        sm.collider2D.enabled = true;
    }
}
