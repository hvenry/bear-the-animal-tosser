using UnityEngine;

/// <summary>
/// Reports how many animals of one species are currently inside this pen. Kept as
/// a lightweight query helper for pen logic and debugging.
/// </summary>
public class AnimalDetection : MonoBehaviour
{
    [Tooltip("Tag of the animal species this pen accepts.")]
    public string Tag = "";

    /// <summary>Number of animals of this pen's species currently in the level.</summary>
    public int CountInLevel =>
        string.IsNullOrEmpty(Tag) ? 0 : GameObject.FindGameObjectsWithTag(Tag).Length;
}
