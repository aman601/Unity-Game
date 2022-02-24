using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class netcodeInitializer : MonoBehaviour
{
    public GameObject server;
    public GameObject client;
    // Start is called before the first frame update
    void Start()
    {
        if (MultiMenu.isServer)
        {
            server.SetActive(true);
        }
        else
        {
            client.SetActive(true);
        }
    }

}
