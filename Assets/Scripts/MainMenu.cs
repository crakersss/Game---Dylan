using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartMenu(){
        SceneManager.LoadScene("Start Screen");
    }
    public void PlayGame(){
        SceneManager.LoadScene("Main");
    }

    public void HowTo(){
        SceneManager.LoadScene("How To Play");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
