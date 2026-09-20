using UnityEngine;

/// <summary>
/// Lets the bear carry a stack of animals and throw them. Right click picks up the
/// nearest animal in range, left click throws the bottom of the stack along an arc
/// toward the cursor.
///
/// Stacking rule: an animal can only be added if it is no heavier than everything
/// already carried, so the bear cannot balance a cow on top of a chicken.
/// </summary>
public class PickUp : MonoBehaviour
{
    [Header("Carry slots, bottom of the stack first")]
    public Transform holdSpot;
    public Transform holdSpot2;
    public Transform holdSpot3;
    public Transform holdSpot4;

    [Header("Pick up")]
    [Tooltip("Radius around the bear searched for an animal to pick up.")]
    public float pickUpRadius = 2f;

    [Tooltip("Layers that count as pickable animals.")]
    public LayerMask pickUpMask;

    [Header("Audio")]
    public AudioClip pickUpAudioClip;
    public AudioClip throwingAudioClip;
    public AudioClip errorAudio;

    private Transform[] _spots;
    private GameObject[] _holdings;
    private AudioSource _audioSource;

    private void Start()
    {
        _spots = new[] { holdSpot, holdSpot2, holdSpot3, holdSpot4 };
        _holdings = new GameObject[_spots.Length];
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryPickUp();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryThrow();
        }
    }

    private void TryPickUp()
    {
        Collider2D candidate = Physics2D.OverlapCircle(transform.position, pickUpRadius, pickUpMask);
        if (candidate == null)
        {
            return;
        }

        MovementSM animal = candidate.GetComponent<MovementSM>();
        if (animal == null)
        {
            return;
        }

        int slot = FirstFreeSlot();
        if (slot < 0 || !CanStack(animal))
        {
            PlayClip(errorAudio);
            return;
        }

        GameObject picked = candidate.gameObject;
        picked.transform.position = _spots[slot].position;
        picked.transform.parent = transform;
        picked.GetComponent<Rigidbody2D>().simulated = false;
        animal.ChangeState(animal.heldState);

        _holdings[slot] = picked;
        PlayClip(pickUpAudioClip);
    }

    private void TryThrow()
    {
        GameObject thrown = _holdings[0];
        if (thrown == null)
        {
            return;
        }

        MovementSM animal = thrown.GetComponent<MovementSM>();
        thrown.transform.parent = null;
        thrown.GetComponent<Rigidbody2D>().simulated = true;
        animal.ChangeState(animal.thrownState);

        PlayClip(throwingAudioClip);
        ShuffleStackDown();
    }

    /// <summary>
    /// Moves every carried animal down one slot after a throw, so the stack stays
    /// packed from the bottom and each animal sits on its new hold spot.
    /// </summary>
    private void ShuffleStackDown()
    {
        for (int i = 0; i < _holdings.Length - 1; i++)
        {
            _holdings[i] = _holdings[i + 1];
            if (_holdings[i] != null)
            {
                _holdings[i].transform.position = _spots[i].position;
            }
        }

        _holdings[_holdings.Length - 1] = null;
    }

    private int FirstFreeSlot()
    {
        for (int i = 0; i < _holdings.Length; i++)
        {
            if (_holdings[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// True when <paramref name="candidate"/> is no heavier than every animal
    /// already being carried.
    /// </summary>
    private bool CanStack(MovementSM candidate)
    {
        foreach (GameObject held in _holdings)
        {
            if (held == null)
            {
                continue;
            }

            MovementSM carried = held.GetComponent<MovementSM>();
            if (carried != null && carried.weight < candidate.weight)
            {
                return false;
            }
        }

        return true;
    }

    private void PlayClip(AudioClip clip)
    {
        if (_audioSource != null && clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }

    // Shows the pick-up radius in the editor.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
