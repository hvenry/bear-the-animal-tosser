using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // load the biggermap scene
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadLevel1(){
        // load the biggermap scene
        SceneManager.LoadScene("BiggerMap");
    }

    public void LoadLevel2(){
        // load level 2
        SceneManager.LoadScene("BiggerMap 2");
    }

    
    public void LoadLevel3(){
        // load level 3
        SceneManager.LoadScene("BiggerMap 3");
    }


    // public void GoToLevelSelect(){
    //     SceneManager.LoadScene("LevelSelect");
    // }

    public void GoToSettingsMenu(){
        SceneManager.LoadScene("OptionsMenu");
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        // quit the game
        Application.Quit();
    }
}
