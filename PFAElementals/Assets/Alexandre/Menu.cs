using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] RectTransform triangle;


    Vector3 high = new Vector3(-250, 100, 0);
    Vector3 middle = new Vector3(-250, 0, 0);
    Vector3 down = new Vector3(-250, -100, 0);


    private int placement = 1;
    private bool axisUsed = false;

    [SerializeField] float t = 1f;


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
                        triangle.localPosition= Vector3.Lerp(transform.position,middle, t);
                        placement = 2;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        triangle.localPosition = Vector3.Lerp(transform.position, down, t);
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
                        triangle.localPosition = Vector3.Lerp(transform.position, down, t);
                        placement = 3;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        triangle.localPosition = Vector3.Lerp(transform.position, high, t);
                        placement = 1;
                        axisUsed = true;
                    }
                    break;

                case 3:
                    if (Input.GetAxisRaw("VerticalP") < 0)
                    {
                        triangle.localPosition = Vector3.Lerp(transform.position, high, t) ;
                        placement = 1;
                        axisUsed = true;
                    }
                    else if (Input.GetAxisRaw("VerticalP") > 0)
                    {
                        triangle.localPosition = Vector3.Lerp(transform.position, middle, t);
                        placement = 2;
                        axisUsed = true;
                    }
                    else if (Input.GetKeyDown("joystick 1 button 0"))
                    {
                        Application.Quit();
                    }
                    break;
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
