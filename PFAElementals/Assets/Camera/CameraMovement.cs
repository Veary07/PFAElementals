using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] Transform playerOne = null;
    [SerializeField] Transform playerTwo;
    [SerializeField] float lerpPerc;
    [SerializeField] float zoomSpeed = 50f;

    [SerializeField] float clampMaxZ = -20f;
    [SerializeField] float clampMinZ = -40f;

    [SerializeField] float clampMaxY = 40f;
    [SerializeField] float clampMinY = 10f;


    Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3((playerOne.transform.position.x + playerTwo.transform.position.x) * 0.5f, transform.position.y, transform.position.z), Time.deltaTime * lerpPerc);
    }

    private void LateUpdate()
    {
        Vector3 playerOneScreenPosition = cam.WorldToScreenPoint(playerOne.position);
        Vector3 playerTwoScreenPosition = cam.WorldToScreenPoint(playerTwo.position);

        float playerMinX;
        float playerMaxX;

        if (playerOneScreenPosition.x > playerTwoScreenPosition.x)
        {
            playerMaxX = playerOneScreenPosition.x;
            playerMinX = playerTwoScreenPosition.x;
        } 
        else
        {
            playerMinX = playerOneScreenPosition.x;
            playerMaxX = playerTwoScreenPosition.x;

        }

        if ((playerMaxX - playerMinX) > 400f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + (zoomSpeed * Time.deltaTime), clampMinY, clampMaxY), Mathf.Clamp(transform.position.z - (zoomSpeed * Time.deltaTime), clampMinZ, clampMaxZ)), Time.deltaTime * lerpPerc);
        }

        else if ((playerMaxX - playerMinX) < 200f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - (zoomSpeed * Time.deltaTime), clampMinY, clampMaxY), Mathf.Clamp(transform.localPosition.z + (zoomSpeed * Time.deltaTime), clampMinZ, clampMaxZ)), Time.deltaTime * lerpPerc);
        }


    }
}
