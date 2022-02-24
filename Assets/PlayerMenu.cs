using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    public GameObject playersObject;
    public GameObject menuObject;
    public GameObject multiObject;
    public void singlePlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void multiPlayer()
    {
        playersObject.SetActive(false);
        multiObject.SetActive(true);
    }

    public void back()
    {
        playersObject.SetActive(false);
        menuObject.SetActive(true);
    }
}
