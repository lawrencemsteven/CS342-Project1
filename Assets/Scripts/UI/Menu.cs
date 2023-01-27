using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void HowtoPlay()
    {
        Debug.Log("How to play Scene");
        //Scene Manager load to how to play scene
    }

    public void QuitGame() 
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
