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
        SceneManager.LoadScene("HowToPlay");
    }

    public void QuitGame() 
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void NextHowTo() 
    {
        SceneManager.LoadScene("HowToPlay2");
    }
}
