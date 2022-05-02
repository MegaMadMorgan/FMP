using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // load game scene
        SceneManager.LoadScene("City_Centre", LoadSceneMode.Single);
    }

    public void BackToMainMenu()
    {
        // load main menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        // quit game
        Application.Quit();
    }
}
