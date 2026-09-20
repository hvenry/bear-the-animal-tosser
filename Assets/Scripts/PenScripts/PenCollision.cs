using UnityEngine;

/// <summary>
/// Sits on a pen's collider. When an animal of the matching species touches the
/// pen it stops colliding with it (so it settles inside) and is reported safe.
/// </summary>
public class PenCollision : MonoBehaviour
{
    [Tooltip("Tag of the animal species this pen accepts.")]
    public string TagToIgnore = "";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(TagToIgnore))
        {
            return;
        }

        Collider2D animalCollider = collision.gameObject.GetComponent<Collider2D>();
        Collider2D penCollider = GetComponent<Collider2D>();
        if (animalCollider != null && penCollider != null)
        {
            Physics2D.IgnoreCollision(animalCollider, penCollider);
        }

        MovementSM movement = collision.gameObject.GetComponent<MovementSM>();
        if (movement != null)
        {
            EventManager.RaiseSafe(movement.id);
        }
    }
}
