using UnityEngine;

/// <summary>
/// Evaluates a point along a parabolic arc between two positions. Used to fly a
/// thrown animal from the bear's hands to where the player aimed.
/// </summary>
public static class MathParabola
{
    /// <summary>
    /// Point on the arc at <paramref name="t"/> in 0..1, where
    /// <paramref name="height"/> is how far the arc bulges upward at its midpoint.
    /// </summary>
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        // Peaks at t = 0.5 and is zero at both ends.
        float arc = -4f * height * t * t + 4f * height * t;

        Vector3 mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, arc + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        float arc = -4f * height * t * t + 4f * height * t;

        Vector2 mid = Vector2.Lerp(start, end, t);
        return new Vector2(mid.x, arc + Mathf.Lerp(start.y, end.y, t));
    }
}
