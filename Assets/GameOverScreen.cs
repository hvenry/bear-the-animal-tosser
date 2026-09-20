using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup(){
         gameObject.SetActive(true);
    }

    // button for restart
    public void RestartButton(){
        SceneManager.LoadScene("LevelSelect");
    }

    public void ExitButton(){
        SceneManager.LoadScene("Main");
    }
}

