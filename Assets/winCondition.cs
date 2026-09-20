using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Drives the win/loss rules for a level: tracks how many loose animals have been
/// penned, counts down the level timer, and draws the on-screen HUD.
/// </summary>
public class winCondition : MonoBehaviour
{
    // Tags of every animal species that counts toward the level goal.
    private static readonly string[] AnimalTags = { "Cow", "Pig", "Hog", "Chicken" };

    // Animals are spawned by Spawner during its own Start(), so the roster is
    // gathered one frame later rather than racing it.
    private const float RosterScanDelay = 1f;

    [SerializeField] public float totalTime;
    public AudioSource success;

    private readonly List<GameObject> _loose = new List<GameObject>();
    private bool[] _isSafe = Array.Empty<bool>();

    private int _total;
    private int _safeCount;
    private float _timeRemaining;

    // Cached so OnGUI does not allocate a new GUIStyle every frame.
    private GUIStyle _hudStyle;

    private void Start()
    {
        success = GetComponent<AudioSource>();
        _timeRemaining = totalTime;
        Invoke(nameof(CollectAnimals), RosterScanDelay);
    }

    private void OnDestroy()
    {
        // Without this the delegate keeps a dead instance alive across scene loads
        // and Switch() fires on a destroyed object.
        EventManager.onSafe -= Switch;
    }

    /// <summary>
    /// Builds the roster of loose animals and hands each one the index it reports
    /// back through <see cref="EventManager.onSafe"/> when it reaches a pen.
    /// </summary>
    private void CollectAnimals()
    {
        foreach (string tag in AnimalTags)
        {
            _loose.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }

        _total = _loose.Count;
        _isSafe = new bool[_total];

        for (int i = 0; i < _total; i++)
        {
            MovementSM movement = _loose[i].GetComponent<MovementSM>();
            if (movement != null)
            {
                movement.id = i;
            }
        }

        EventManager.onSafe += Switch;
    }

    /// <summary>
    /// Marks one animal as penned. Guards against an animal scoring twice, which
    /// would otherwise let the level be won with animals still loose.
    /// </summary>
    private void Switch(int id)
    {
        if (id < 0 || id >= _isSafe.Length || _isSafe[id])
        {
            return;
        }

        _isSafe[id] = true;
        _safeCount++;

        if (success != null)
        {
            success.Play();
        }
    }

    private void Update()
    {
        _timeRemaining -= Time.deltaTime;

        if (Won())
        {
            SceneManager.LoadScene("Win");
        }
        else if (Lost())
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnGUI()
    {
        // Clamped so the HUD never shows a negative clock on the frame the level ends.
        float shown = Mathf.Max(0f, _timeRemaining);
        int minutes = Mathf.FloorToInt(shown / 60f);
        int seconds = Mathf.FloorToInt(shown % 60f);

        string timeText = string.Format("<color=white>TIME: {0:00}:{1:00}</color>", minutes, seconds);
        string progressText = string.Format("<color=white>ANIMALS WRANGLED: {0}/{1}</color>", _safeCount, _total);

        _hudStyle ??= new GUIStyle { fontSize = 24, richText = true };

        Vector2 size = _hudStyle.CalcSize(new GUIContent(timeText));
        GUI.Label(new Rect((Screen.width - size.x) / 2f, 30f, size.x, 40f), timeText, _hudStyle);
        GUI.Label(new Rect(20f, Screen.height - 50f, 250f, 40f), progressText, _hudStyle);
    }

    // _total > 0 keeps the level from being "won" during the frames before the
    // roster has been collected.
    private bool Won() => _total > 0 && _safeCount >= _total;

    private bool Lost() => _timeRemaining <= 0f;
}
