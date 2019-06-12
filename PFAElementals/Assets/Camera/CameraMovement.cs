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

    private bool maxZoomedInYReached = false;
    private bool maxZoomedOutYReached = false;
    private bool maxZoomedInZReached = false;
    private bool maxZoomedOutZReached = false;



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

        Debug.Log(Vector3.Distance(playerOneScreenPosition, playerTwoScreenPosition));

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

        if (clampMaxY - transform.position.y > 1f)
        {
            maxZoomedOutYReached = false;
        }
        if (clampMaxZ - transform.position.z > 1f)
        {
            maxZoomedOutZReached = false;
        }
        if (transform.position.y - clampMinY > 1f)
        {
            maxZoomedInYReached = false;
        }
        if (transform.position.z - clampMinZ > 1f)
        {
            maxZoomedInZReached = false;
        }

        if (Vector3.Distance(playerOneScreenPosition, playerTwoScreenPosition) > zoomOut)
        {
            if (!maxZoomedOutYReached)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + (zoomSpeed * Time.deltaTime), clampMinY, clampMaxY), transform.position.z), Time.deltaTime * lerpPerc);
            }

            if (!maxZoomedOutZReached)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z - (zoomSpeed * Time.deltaTime), clampMinZ, clampMaxZ)), Time.deltaTime * lerpPerc);
            }

            //if (!maxZoomedOutReached)
            //{
            //    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + (zoomSpeed * Time.deltaTime), clampMinY, clampMaxY), Mathf.Clamp(transform.position.z - (zoomSpeed * Time.deltaTime), clampMinZ, clampMaxZ)), Time.deltaTime * lerpPerc);
            //}

            if (clampMaxY - transform.position.y < 1f)
            {
                maxZoomedOutYReached = true;
            }

            if (clampMaxZ - transform.position.z < 1f)
            {
                maxZoomedOutZReached = true;
            }
        }

        else if (Vector3.Distance(playerOneScreenPosition, playerTwoScreenPosition) < zoomIn)
        {
            maxZoomedOutYReached = false;
            maxZoomedOutZReached = false;

            if (!maxZoomedInYReached)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - (zoomSpeed * Time.deltaTime), clampMinY, clampMaxY), transform.position.z), Time.deltaTime * lerpPerc);
            }

            if (!maxZoomedInZReached)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.localPosition.z + (zoomSpeed * Time.deltaTime), clampMinZ, clampMaxZ)), Time.deltaTime * lerpPerc);
            }


            //if (!maxZoomedInReached)
            //{
            //    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - (zoomSpeed * Time.deltaTime), clampMinY, clampMaxY), Mathf.Clamp(transform.localPosition.z + (zoomSpeed * Time.deltaTime), clampMinZ, clampMaxZ)), Time.deltaTime * lerpPerc);
            //}
            if (transform.position.y - clampMinY < 1f)
            {
                maxZoomedInYReached = true;
            }
            if (transform.position.z - clampMinZ < 1f)
            {
                maxZoomedInZReached = true;
            }
        }


    }
}
