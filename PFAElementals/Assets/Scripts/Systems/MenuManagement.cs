using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    // Update is called once per frame
    public int valor;

    void Update()
    {

    }

    public void PlayScene()
    {

     SceneManager.LoadScene("Alexandre");

    }

    public void Quit()
    {
         Application.Quit();
    }
}
