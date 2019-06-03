﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerNumber = 1;
    [SerializeField] RespawnManager respawnManager;

    public HealthManager health;
    public GunController gun;
    [SerializeField] private Timer timer;
    [SerializeField] private Monolith monolith; 

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Vector3 playerDirection;

    private bool canBuild = false;
    private bool isBuilding = false;
    private Transform buildZone = null;
    [SerializeField] float buildingDuration = 1f;
    [SerializeField] private Timer buildingTimer;

    private float rayLenght;
    private float moveSpeed;
    [SerializeField] private float startingMoveSpeed = 25.0F;

    BuildingZone buildingZone = null;

    [SerializeField] float decal = 0.05f;

    [SerializeField] AnimationCurve accelerationCurve;
    [SerializeField] Timer dashTimer;
    [SerializeField] float dashDuration;

    #region DashMove
    [SerializeField] private float dashCoolDown = 2f;
    private bool canDash = true;


    #endregion

    #region CrowdControl
    bool canMove = true;
    bool canSpell = true;
    #endregion

    #region Spells

    #region Shield 
    public ShieldSpell shield;
    private bool shieldDuration = false;
    private bool shieldCoolDown = false;
    #endregion


    #endregion

    float leftStickX;
    float leftStickY;

    Animator anim;

    void Start()
    {
        moveSpeed = startingMoveSpeed;
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float f = Input.GetAxisRaw("Gach");
        float f2 = Input.GetAxisRaw("Gach 2");

        Debug.Log("f = " + f);

        if (canSpell)
        {
            if (playerNumber == 1)
            {
                if (Input.anyKey)
                {
                    playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalP") + Vector3.forward * Input.GetAxisRaw("VerticalP");
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);

                }

                if (f == -1)
                {
                    gun.damageBallTrigger = true;
                }


                if (Input.GetButtonDown("X"))

                if (Input.GetAxis("LT") > 0)

                {
                    shield.CastShield(health);
                }
            }

            if (playerNumber == 2)
            {
                if (Input.anyKey)
                {
                    playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalP") + Vector3.forward * Input.GetAxisRaw("VerticalP");
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                }


                if (f2 == -1)
                 {
                     gun.damageBallTrigger = true;
                 }
                 
                if (Input.GetButtonDown("X 2"))

                if (Input.GetKeyUp("joystick 2 button 4"))
                {
                    gun.damageBallTrigger = true;
                }

                if (Input.GetAxis("LT 2") > 0)

                {
                    shield.CastShield(health);
                }
            }
        }


        if (canBuild)
        {
            if (playerNumber == 1)
            {

                if (Input.GetButton("B"))

                    if (Input.GetAxis("RT") > 0)

                    {
                        anim.SetInteger("condition", 2);
                        if (!isBuilding)
                        {
                            buildingTimer.SetDuration(buildingDuration, 1);
                            isBuilding = true;
                        }

                        canMove = false;
                        canSpell = false;
                        moveVelocity = new Vector3(0, 0, 0);

                        if (isBuilding && buildingTimer.Update())
                        {
                            respawnManager.AddMonolith(playerNumber, buildingZone.transform);
                            buildingZone.SetTeamAndIndex(playerNumber, respawnManager.GetListCount(playerNumber) - 1);
                            isBuilding = false;
                        }
                    }

                    else if (Input.GetButtonUp("B"))
                    {
                        canMove = true;
                        canSpell = true;
                        anim.SetInteger("condition", 0);
                    }
                    else if (Input.GetAxis("RT") == 0)
                    {
                        canMove = true;
                        canSpell = true;
                        buildingTimer.ResetCurrentTime();
                    }
            }

            if (playerNumber == 2)
            {

                if (Input.GetButtonDown("B 2"))

                //Input.GetKey("joystick 2 button 8") && Input.GetKey("joystick 2 button 9")
                {
                    if (!isBuilding)
                    {
                        buildingTimer.SetDuration(buildingDuration, 1);
                        isBuilding = true;
                    }
                    canMove = false;
                    canSpell = false;

                    if (isBuilding && buildingTimer.Update())
                    {
                        respawnManager.AddMonolith(playerNumber, buildingZone.transform);
                        buildingZone.SetTeamAndIndex(playerNumber, respawnManager.GetListCount(playerNumber) - 1);
                        isBuilding = false;
                    }
                }

                else if (Input.GetButtonUp("B 2"))

                {
                    canMove = true;
                    canSpell = true;
                    buildingTimer.ResetCurrentTime();
                }
            }
        }

        else
        {
            canMove = true;
            canSpell = true;
        }

        #region Mobility
        if (canMove)
        {
            if (playerNumber == 1)
            {

                if ((f == 1 && canDash) && (Input.GetAxisRaw("HorizontalP") !=0.0f || Input.GetAxisRaw("VerticalP") !=0.0f))
                if ((Input.GetKeyDown("joystick 1 button 5") && canDash) && ((Input.GetAxisRaw("HorizontalP") !=0.0f || Input.GetAxisRaw("VerticalP") !=0.0f)))
                {
                    StartCoroutine(DashMove());
                }
                moveInput = new Vector3(Input.GetAxisRaw("HorizontalP"), 0f, Input.GetAxisRaw("VerticalP")).normalized;

                if ((Vector3.right * Input.GetAxisRaw("HorizontalR") + Vector3.forward * Input.GetAxisRaw("VerticalR")).sqrMagnitude <= 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(moveInput, Vector3.up);
                }
                if (Input.GetAxisRaw("HorizontalP") != 0.0f || Input.GetAxisRaw("VerticalP") != 0.0f)
                {
                    anim.SetInteger("condition", 1);
                }
                else if (Input.GetAxisRaw("HorizontalP") == 0.0f || Input.GetAxisRaw("VerticalP") == 0.0f)
                {
                    anim.SetInteger("condition", 0);
                }
            }


            else if (playerNumber == 2)
            {

                if (f2 == 1 && canDash)

                if ((Input.GetKeyDown("joystick 2 button 5") && canDash) && ((Input.GetAxisRaw("HorizontalP 2") != 0.0f || Input.GetAxisRaw("VerticalP 2") != 0.0f)))

                {
                    StartCoroutine(DashMove());
                }
                moveInput = new Vector3(Input.GetAxisRaw("HorizontalP 2"), 0f, Input.GetAxisRaw("VerticalP 2")).normalized;
                if ((Vector3.right * Input.GetAxisRaw("HorizontalR 2") + Vector3.forward * Input.GetAxisRaw("VerticalR 2")).sqrMagnitude <= 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(moveInput, Vector3.up);
                }
                if (Input.GetAxisRaw("HorizontalP 2") != 0.0f || Input.GetAxisRaw("VerticalP 2") != 0.0f)
                {
                    anim.SetInteger("condition", 1);
                }
                else if (Input.GetAxisRaw("HorizontalP 2") == 0.0f || Input.GetAxisRaw("VerticalP 2") == 0.0f)
                {
                    anim.SetInteger("condition", 0);
                }
            }


            else if (playerNumber == 3)
            {
                moveInput = new Vector3(Input.GetAxisRaw("HorizontalP 3"), 0f, Input.GetAxisRaw("VerticalP 3")).normalized;
            }


            else if (playerNumber == 4)
            {
                moveInput = new Vector3(Input.GetAxisRaw("HorizontalP 4"), 0f, Input.GetAxisRaw("VerticalP 4")).normalized;
            }
        }
        

        

        moveVelocity = moveInput * moveSpeed;

        #endregion
        //rotate with controller
        #region Rotating

        if (playerNumber == 1)
            {
                playerDirection = Vector3.right * (Input.GetAxisRaw("HorizontalR")) + Vector3.forward * (Input.GetAxisRaw("VerticalR"));
            }

            if (playerNumber == 2)
            {
                playerDirection = Vector3.right * (Input.GetAxisRaw("HorizontalR 2")) + Vector3.forward * (Input.GetAxisRaw("VerticalR 2"));
            }

            if (playerNumber == 3)
            {
                playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalR 3") + Vector3.forward * Input.GetAxisRaw("VerticalR 3");
            }

            if (playerNumber == 4)
            {
                playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalR 4") + Vector3.forward * Input.GetAxisRaw("VerticalR 4");
            }


            if (playerDirection.sqrMagnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection,Vector3.up);   
                gun.isFiring = true;

            }
            else
            {
                gun.isFiring = false;
            }
        #endregion 
    }
    public void SetShieldDurationOn()
    {
        shieldDuration = true;
    }
    public void SetShieldCoolDownOn()
    {
        shieldCoolDown = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }

    public int TeamNumber()
    {
        return playerNumber;
    }

    private void DashMove(Vector3 direction)
    {

    }

    IEnumerator DashMove()
    {
        canDash = false;
        dashTimer.SetDuration(dashDuration, 1);
        while (!dashTimer.Update())
        {
            moveVelocity *= accelerationCurve.Evaluate(dashTimer.Progress());
            yield return null;
        }
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    public int GetTeam()
    {
        return playerNumber;
    }

    public void CanBuild(Transform _buildZone)
    {
        canBuild = true;
        buildZone = _buildZone;
    }
    public void CanNotBuild()
    {
        canBuild = false;
        buildZone = null;
    }

    public void SetBuildingZone(BuildingZone zone)
    {
        buildingZone = zone.gameObject.GetComponent<BuildingZone>();
    }

    public void UnSetBuildingZone()
    {
        buildingZone = null;
    }

}


