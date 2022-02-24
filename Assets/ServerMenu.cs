using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//https://stackoverflow.com/questions/51975799/how-to-get-ip-address-of-device-in-unity-2018

public class ServerMenu : MonoBehaviour
{
    public GameObject serverObject;
    public GameObject multiObject;
    public TextMeshProUGUI ipObject;
    void Start()
    {
        string ipv4 = IPManager2.GetIP(ADDRESSFAM.IPv4);
        ipv4 = "IP: " + ipv4;
        Debug.Log("IP: " + ipv4);
        ipObject.text = ipv4;
    }


    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    public void back()
    {
        serverObject.SetActive(false);
        multiObject.SetActive(true);
    }
}
