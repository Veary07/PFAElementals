using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick 1 button 0"))
        {
            SceneManager.LoadScene("Alexandre");
        }
        if (Input.GetKeyDown("joystick 1 button 1"))
        {
            Application.Quit();
        }
    }
}
