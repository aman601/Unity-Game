using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonStaticNetcode : MonoBehaviour
{

    public GameObject otherCar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void log() {
        Debug.Log("log");
    }
    public void createOtherCar() {
        Debug.Log("non static about to instantiate");
        GameObject.Instantiate(otherCar, new Vector3(5, 5, 5), Quaternion.identity);
        Debug.Log("Other Car Created");
    }
}
