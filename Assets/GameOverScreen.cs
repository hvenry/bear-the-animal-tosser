using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Buttons on the game-over screen.
/// </summary>
public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    /// <summary>Back to level select to try again.</summary>
    public void RestartButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    /// <summary>Back to the main menu.</summary>
    public void ExitButton()
    {
        SceneManager.LoadScene("Main");
    }
}
