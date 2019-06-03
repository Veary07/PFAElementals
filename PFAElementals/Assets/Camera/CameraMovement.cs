using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] Transform playerOne = null;
    [SerializeField] Transform playerTwo;
    [SerializeField] float lerpPerc;
    [SerializeField] float zoomSpeed = 50f;

    Camera cam;
    private Vector3 previousCameraPosition = new Vector3(0,0,0);

    [SerializeField] float clampMinY = 20f;
    [SerializeField] float clampMaxY = 40f;
    [SerializeField] float clampMinZ = -40f;
    [SerializeField] float clampMaxZ = -20f;

    [SerializeField] float zoomIn = 200f;
    [SerializeField] float zoomOut = 400f;

    private float minZ = 0f;
    [SerializeField] float ZOffset = 20f;

    private bool maxZoomedInReached = false;
    private bool maxZoomedOutReached = false;


    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (playerOne.transform.position.z > playerTwo.transform.position.z)
        {
            minZ = playerTwo.transform.position.z;
        }
        else
        {
            minZ = playerOne.transform.position.z;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3((playerOne.transform.position.x + playerTwo.transform.position.x) * 0.5f, transform.position.y, minZ - ZOffset), Time.deltaTime * lerpPerc);
        transform.LookAt(Vector3.Lerp(previousCameraPosition, new Vector3((playerOne.transform.position.x + playerTwo.transform.position.x) * 0.5f, (playerOne.transform.position.y + playerTwo.transform.position.y) * 0.5f, (playerOne.transform.position.z + playerTwo.transform.position.z) * 0.5f), Time.deltaTime * lerpPerc));
        previousCameraPosition = new Vector3((playerOne.transform.position.x + playerTwo.transform.position.x) * 0.5f, (playerOne.transform.position.y + playerTwo.transform.position.y) * 0.5f, (playerOne.transform.position.z + playerTwo.transform.position.z) * 0.5f);
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

        if (Vector3.Distance(playerOne.transform.position, playerTwo.transform.position) > zoomOut)
        {
            maxZoomedInReached = false;
            if (!maxZoomedOutReached)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + (zoomSpeed * Time.deltaTime), clampMinY, clampMaxY), Mathf.Clamp(transform.position.z - (zoomSpeed * Time.deltaTime), clampMinZ, clampMaxZ)), Time.deltaTime * lerpPerc);
            }
            if (transform.position.y - clampMaxY < 1f || transform.position.z - clampMaxZ < 1f)
            {
                maxZoomedOutReached = true;
            }
        }

        else if (Vector3.Distance(playerOne.transform.position, playerTwo.transform.position) < zoomIn)
        {
            maxZoomedOutReached = false;
            if (!maxZoomedInReached)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - (zoomSpeed * Time.deltaTime), clampMinY, clampMaxY), Mathf.Clamp(transform.localPosition.z + (zoomSpeed * Time.deltaTime), clampMinZ, clampMaxZ)), Time.deltaTime * lerpPerc);
            }
            if (transform.position.y - clampMinY < 1f || transform.position.z - clampMinZ < 1f)
            {
                maxZoomedInReached = true;
            }
        }


    }
}
