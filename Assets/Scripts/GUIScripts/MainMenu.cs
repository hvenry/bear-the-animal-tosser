using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene navigation for the menu screens. Each method is wired to a UI button.
/// </summary>
public class MainMenu : MonoBehaviour
{
    public void PlayGame() => SceneManager.LoadScene("LevelSelect");

    public void LoadLevel1() => SceneManager.LoadScene("BiggerMap");

    public void LoadLevel2() => SceneManager.LoadScene("BiggerMap 2");

    public void LoadLevel3() => SceneManager.LoadScene("BiggerMap 3");

    public void GoToSettingsMenu() => SceneManager.LoadScene("OptionsMenu");

    public void GoToMainMenu() => SceneManager.LoadScene("Main");

    /// <summary>
    /// Quits the built game. Has no effect in the editor or in a WebGL build.
    /// </summary>
    public void QuitGame() => Application.Quit();
}
