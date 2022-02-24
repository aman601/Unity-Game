using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClientMenu : MonoBehaviour
{
    public GameObject clientObject;
    public GameObject multiObject;
    public GameObject ipObject;
    public static string ipaddr;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateIP()
    {
        ipaddr = ipObject.GetComponent<TMP_InputField>().text;
        Debug.Log("IP: " + ipaddr);
    }



    public void back()
    {
        clientObject.SetActive(false);
        multiObject.SetActive(true);
    }
}
