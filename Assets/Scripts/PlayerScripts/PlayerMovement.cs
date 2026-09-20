using UnityEngine;

/// <summary>
/// Top-down WASD/arrow movement for the bear, and the walk/idle animator state.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Movement speed in world units per second.")]
    public float moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 _moveDirection;

    private void Update()
    {
        ReadInput();
    }

    // Physics runs on a fixed step, so the actual move happens here.
    private void FixedUpdate()
    {
        rb.velocity = _moveDirection * moveSpeed;
    }

    private void ReadInput()
    {
        // GetAxisRaw gives -1/0/1 with no smoothing, which suits grid-ish movement.
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (animator != null)
        {
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);
            animator.SetFloat("Speed", new Vector2(moveX, moveY).sqrMagnitude);
        }

        // Normalized so diagonal movement is not faster than cardinal movement.
        _moveDirection = new Vector2(moveX, moveY).normalized;
    }
}
