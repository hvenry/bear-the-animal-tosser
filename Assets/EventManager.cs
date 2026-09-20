using UnityEngine;

/// <summary>
/// Broadcast channel between the pens and the level's win tracker. A pen raises
/// <see cref="onSafe"/> with an animal's id; winCondition counts it.
///
/// Kept as a MonoBehaviour because the level scenes attach it to a scene object;
/// the event itself is static, so pens do not need a reference to that object.
/// </summary>
public class EventManager : MonoBehaviour
{
    public delegate void OnSafe(int id);

    public static OnSafe onSafe;

    /// <summary>
    /// Raises the event only if something is listening, so a pen collision that
    /// happens before the win tracker subscribes cannot throw.
    /// </summary>
    public static void RaiseSafe(int id)
    {
        onSafe?.Invoke(id);
    }
}
