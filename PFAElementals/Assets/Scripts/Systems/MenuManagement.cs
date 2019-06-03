using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("A"))
        {
            SceneManager.LoadScene("Alexandre");
        }
        if (Input.GetKeyDown("B"))
        {
            Application.Quit();
        }
    }
}
