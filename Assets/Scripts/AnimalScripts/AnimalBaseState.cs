using UnityEngine;

/// <summary>
/// Shared base for the animal states. Holds the typed state-machine reference and
/// the player transform so each state does not re-run FindWithTag on its own, and
/// provides the "how far am I from the player" test they all share.
/// </summary>
public abstract class AnimalBaseState : BaseState
{
    protected readonly MovementSM sm;

    protected AnimalBaseState(string name, MovementSM stateMachine) : base(name, stateMachine)
    {
        sm = stateMachine;
    }

    /// <summary>Player transform, resolved once per animal by the state machine.</summary>
    protected Transform Target => sm.player;

    /// <summary>True once the player has been found in the scene.</summary>
    protected bool HasTarget => sm.player != null;

    /// <summary>
    /// Vector pointing from the player toward this animal. Zero when there is no
    /// player in the scene, which leaves the animal where it is rather than
    /// throwing every frame.
    /// </summary>
    protected Vector2 AwayFromPlayer =>
        HasTarget ? sm.rigidbody.position - (Vector2)sm.player.position : Vector2.zero;

    /// <summary>
    /// True when the player is close enough to spook this animal. Compares squared
    /// magnitudes to avoid a square root every frame.
    /// </summary>
    protected bool PlayerIsNear =>
        HasTarget && AwayFromPlayer.sqrMagnitude < sm.fleeThreshold;

    /// <summary>Moves the animal along <paramref name="direction"/> at its speed.</summary>
    protected void MoveAlong(Vector2 direction)
    {
        sm.rigidbody.MovePosition(
            sm.rigidbody.position + direction.normalized * (sm.speed * Time.deltaTime));
    }
}
