using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject settingsObject;
    public void back()
    {
        settingsObject.SetActive(false);
        menuObject.SetActive(true);
    }
}
