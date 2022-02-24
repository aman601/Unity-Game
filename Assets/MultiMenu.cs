using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiMenu : MonoBehaviour
{
    public GameObject playersObject;
    public GameObject multiObject;
    public GameObject serverObject;
    public GameObject clientObject;
    public static bool isServer = false;
    public void next()
    {
        if (isServer)
        {
            multiObject.SetActive(false);
            serverObject.SetActive(true);
        }
        else
        {
            multiObject.SetActive(false);
            clientObject.SetActive(true);
        }
    }

    public void toggle()
    {
        isServer = !isServer;
    }

    public void back()
    {
        multiObject.SetActive(false);
        playersObject.SetActive(true);
    }
}
