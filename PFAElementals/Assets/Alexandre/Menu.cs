using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] RectTransform curseur;
    [SerializeField] RectTransform curseurOptions;
    [SerializeField] GameObject optionsMenu;

    Vector3 playButton = new Vector3(-250, 100, 0);
    Vector3 optionsButton = new Vector3(-250, 0, 0);
    Vector3 quitButton = new Vector3(-250, -100, 0);

    Vector3 fullScreenButton = new Vector3(-165, 140, 0);
    Vector3 resolutionButton = new Vector3(-440, 30, 0);
    Vector3 graphicsButton = new Vector3(-440, -80, 0);
    Vector3 volumeButton = new Vector3(-440, -210, 0);

    public SettingsMenu settings;

    private bool options = false;
    private bool fullscreen = true;


    private int placement = 1;
    private bool axisUsed = false;

    [SerializeField] float t = 1f;

    private void Start()
    {
        optionsMenu.SetActive(false);
        curseur.localPosition = playButton;
        curseurOptions.localPosition = fullScreenButton;
    }

    // Update is called once per frame
    void Update()
    {
        if (!axisUsed)
        {
            switch (placement)
            {
                case 1:
                    if (Input.GetAxisRaw("VerticalP") < 0)
                    {
                        curseur.localPosition = optionsButton;
                        placement = 2;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        curseur.localPosition = quitButton;
                        placement = 3;
                        axisUsed = true;
                    }
                    else if (Input.GetKeyDown("joystick 1 button 0"))
                    {
                        SceneManager.LoadScene("Alexandre");
                    }
                    break;

                case 2:
                    if (Input.GetAxisRaw("VerticalP") < 0)
                    {
                        curseur.localPosition = quitButton;
                        placement = 3;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        curseur.localPosition = playButton;
                        placement = 1;
                        axisUsed = true;
                    }
                    else if (Input.GetKeyDown("joystick 1 button 0"))
                    {
                        optionsMenu.SetActive(true);
                        options = true;
                        placement = 4;
                    }
                    break;

                case 3:
                    if (Input.GetAxisRaw("VerticalP") < 0)
                    {
                        curseur.localPosition = playButton;
                        placement = 1;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        curseur.localPosition = optionsButton;
                        placement = 2;
                        axisUsed = true;
                    }
                    else if (Input.GetKeyDown("joystick 1 button 0"))
                    {
                        Application.Quit();
                    }
                    break;

                case 4:
                    if (Input.GetAxisRaw("VerticalP") < 0)
                    {
                        curseurOptions.localPosition = resolutionButton;
                        placement = 5;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        curseurOptions.localPosition = volumeButton;
                        placement = 7;
                        axisUsed = true;
                    }
                    else if (Input.GetKeyDown("joystick 1 button 0"))
                    {
                        fullscreen = !fullscreen;
                        settings.SetFullScreen(fullscreen);
                    }
                    break;

                case 5:
                    if (Input.GetAxisRaw("VerticalP") < 0)
                    {
                        curseurOptions.localPosition = graphicsButton;
                        placement = 6;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        curseurOptions.localPosition = fullScreenButton;
                        placement = 4;
                        axisUsed = true;
                    }
                    else if (Input.GetKeyDown("joystick 1 button 0"))
                    {
                        Application.Quit();
                    }
                    break;

                case 6:
                    if (Input.GetAxisRaw("VerticalP") < 0)
                    {
                        curseurOptions.localPosition = volumeButton;
                        placement = 7;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        curseurOptions.localPosition = resolutionButton;
                        placement = 5;
                        axisUsed = true;
                    }
                    else if (Input.GetKeyDown("joystick 1 button 0"))
                    {
                        Application.Quit();
                    }
                    break;

                case 7:
                    if (Input.GetAxisRaw("VerticalP") < 0)
                    {
                        curseurOptions.localPosition = fullScreenButton;
                        placement = 4;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        curseurOptions.localPosition = graphicsButton;
                        placement = 6;
                        axisUsed = true;
                    }
                    else if (Input.GetKeyDown("joystick 1 button 0"))
                    {
                        Application.Quit();
                    }
                    break;

            }
        }

        if(options)
        {
            if (Input.GetKeyDown("joystick 1 button 1"))
            {
                curseur.localPosition = playButton;
                curseurOptions.localPosition = fullScreenButton;
                optionsMenu.SetActive(false);
                options = false;
                placement = 1;
            }
        }
    }

    private void LateUpdate()
    {
        if (Input.GetAxisRaw("VerticalP") == 0)
        {
            axisUsed = false;
        }
    }
}
