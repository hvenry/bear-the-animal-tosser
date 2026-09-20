using UnityEngine;

/// <summary>
/// Scatters a random number of animals across a rectangular area when the level
/// loads. Sits alongside the player scripts because it is driven from the level's
/// spawn object.
/// </summary>
public class Spawner : MonoBehaviour
{
    [Header("Animal prefabs")]
    [SerializeField] private GameObject Chicken_Prefab;
    [SerializeField] private GameObject Cow_Prefab;
    [SerializeField] private GameObject Hog_Prefab;
    [SerializeField] private GameObject Pig_Prefab;

    [Header("How many to spawn")]
    [Tooltip("Minimum number of animals, inclusive.")]
    [SerializeField] public int lower;

    [Tooltip("Maximum number of animals, exclusive.")]
    [SerializeField] public int upper;

    [Header("Spawn area")]
    [SerializeField] private Vector2 corner1;
    [SerializeField] private Vector2 corner2;

    private GameObject[] _prefabs;

    private void Start()
    {
        _prefabs = new[] { Chicken_Prefab, Cow_Prefab, Hog_Prefab, Pig_Prefab };

        // Range is [lower, upper) -- kept as the levels were originally tuned.
        int count = Random.Range(lower, upper);
        for (int i = 0; i < count; i++)
        {
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        // Indexing the array rather than a switch means every species can actually
        // be picked; the original switch never reached its last case.
        GameObject prefab = _prefabs[Random.Range(0, _prefabs.Length)];
        if (prefab != null)
        {
            Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
        }
    }

    private Vector2 GetRandomPosition()
    {
        return new Vector2(
            Random.Range(Mathf.Min(corner1.x, corner2.x), Mathf.Max(corner1.x, corner2.x)),
            Random.Range(Mathf.Min(corner1.y, corner2.y), Mathf.Max(corner1.y, corner2.y)));
    }

    // Draws the spawn rectangle in the editor.
    private void OnDrawGizmosSelected()
    {
        Vector3 center = (corner1 + corner2) / 2f;
        Vector3 size = new Vector3(
            Mathf.Abs(corner2.x - corner1.x),
            Mathf.Abs(corner2.y - corner1.y),
            0f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(center, size);
    }
}
