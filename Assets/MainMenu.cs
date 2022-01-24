using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject settingsObject;
    public GameObject playersObject;
    public void playGame()
    {
        menuObject.SetActive(false);
        playersObject.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void settings()
    {
        menuObject.SetActive(false);
        settingsObject.SetActive(true);
    }
}
