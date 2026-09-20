using UnityEngine;

/// <summary>
/// State machine driving one animal. Owns the animal's states and the shared data
/// they read: the cached player transform, movement speed, carry weight and the
/// distance at which the animal spooks.
/// </summary>
public class MovementSM : StateMachine
{
    public Idle idleState;
    public Roaming roamState;
    public Fleeing fleeState;
    public Held heldState;
    public Thrown thrownState;

    [HideInInspector] public new Rigidbody2D rigidbody;
    [HideInInspector] public BoxCollider2D collider2D;

    /// <summary>Player transform, looked up once instead of per state per frame.</summary>
    [HideInInspector] public Transform player;

    /// <summary>Index assigned by winCondition; reported back when this animal is penned.</summary>
    public int id;

    [Tooltip("Movement speed while roaming and fleeing.")]
    public float speed = 10f;

    [Tooltip("Squared distance to the player at which the animal starts to flee.")]
    public float fleeThreshold = 25f;

    [Tooltip("Heavier animals cannot be stacked on top of lighter ones.")]
    public int weight;

    [Tooltip("Base seconds before the animal switches between idling and roaming. " +
             "Randomized slightly per animal at spawn so a herd does not move in lockstep.")]
    public float roamTimer = 2f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Jitter around the authored value so a herd does not move in lockstep.
        roamTimer *= Random.Range(0.75f, 1.5f);

        idleState = new Idle(this);
        roamState = new Roaming(this);
        fleeState = new Fleeing(this);
        heldState = new Held(this);
        thrownState = new Thrown(this);
    }

    protected override BaseState GetInitialState() => idleState;
}
