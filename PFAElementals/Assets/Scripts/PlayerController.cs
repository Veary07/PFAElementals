using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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



    private bool canPlay = true;
    public bool CanPlay
    {
        get { return canPlay; }
        set { canPlay = value; }
    }

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

    [SerializeField] Image dashImage;
    [SerializeField] Image ballImage;
    [SerializeField] Image shieldImage;

    private Animator anim;

    #region DashMove
    [SerializeField] private float dashCoolDown = 2f;
    [SerializeField] private Timer dashCoolDownTimer;
    private bool canDash = true;
    [SerializeField] Timer slowTimer;
    private bool slowed = false;
    [SerializeField] Timer silenceTimer;
    private bool silenced = false;


    #endregion

    #region CrowdControl
    private bool canMove = true;
    private bool canSpell = true;
    #endregion

    #region Spells

    #region Shield 
    public ShieldSpell shield;
    private bool shieldDuration = false;
    private bool shieldCoolDown = false;
    #endregion


    #endregion

    AudioSource source;
    private AudioManagerSO audioManager;

    float leftStickX;
    float leftStickY;

    void Start()
    {
        audioManager = Resources.Load("Sound Holder") as AudioManagerSO;
        source = GameObject.Find("AudioManager").GetComponent<AudioSource>();

        anim = GetComponentInChildren<Animator>();
        moveSpeed = startingMoveSpeed;
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (slowed)
        {
            if(slowTimer.Update())
            {
                slowed = false;
                moveSpeed = startingMoveSpeed;
            }
        }
        if (silenced)
        {
            if (silenceTimer.Update())
            {
                silenced = false;
                canSpell = true;
            }
        }
        if (canPlay)
        {

        if (canSpell)
        {
            if (playerNumber == 1)
            {
                if (Input.anyKey)
                {
                    playerDirection = Vector3.right * Input.GetAxisRaw("HorizontalP") + Vector3.forward * Input.GetAxisRaw("VerticalP");
                    transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                }

                if (Input.GetKeyUp("joystick 1 button 4"))
                {
                    gun.damageBallTrigger = true;
                }

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
                if (Input.GetAxis("RT") > 0)
                {
                    if (!isBuilding)
                    {
                        buildingTimer.SetDuration(buildingDuration, 1);
                        isBuilding = true;
                        anim.SetInteger("condition", 2);
                        source.PlayOneShot(audioManager.building, 1f);
                    }

                    canMove = false;
                    canSpell = false;
                    moveVelocity = new Vector3(0, 0, 0);

                    if (isBuilding && buildingTimer.Update())
                    {
                        source.PlayOneShot(audioManager.totemConstruction);
                        respawnManager.AddMonolith(playerNumber, buildingZone.transform);
                        buildingZone.SetTeamAndIndex(playerNumber, respawnManager.GetListCount(playerNumber) - 1);
                        isBuilding = false;
                        canBuild = false;
                    }
                }
                else if (Input.GetAxis("RT") == 0)
                {
                    canMove = true;
                    canBuild = false;
                    canSpell = true;
                    isBuilding = false;
                    buildingTimer.ResetCurrentTime();
                    anim.SetInteger("condition", 0);

                }
            }

            if (playerNumber == 2)
            {
                //Input.GetKey("joystick 2 button 8") && Input.GetKey("joystick 2 button 9")
                if (Input.GetAxis("RT 2") > 0)
                {

                    if (!isBuilding)
                    {
                        buildingTimer.SetDuration(buildingDuration, 1);
                        isBuilding = true;
                        anim.SetInteger("condition", 2);
                        source.PlayOneShot(audioManager.building, 1f);
                        }

                        canMove = false;
                    canSpell = false;

                    if (isBuilding && buildingTimer.Update())
                    {
                        source.PlayOneShot(audioManager.totemConstruction);
                        respawnManager.AddMonolith(playerNumber, buildingZone.transform);
                        buildingZone.SetTeamAndIndex(playerNumber, respawnManager.GetListCount(playerNumber) - 1);
                        isBuilding = false;
                        canBuild = false;
                    }
                }
                else if (Input.GetAxis("RT 2") == 0)
                {
                    isBuilding = false;
                    canBuild = false;
                    canMove = true;
                    canSpell = true;
                    buildingTimer.ResetCurrentTime();
                    anim.SetInteger("condition", 0);

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
                if ((Input.GetKeyDown("joystick 1 button 5") && canDash) && ((Input.GetAxisRaw("HorizontalP") !=0.0f || Input.GetAxisRaw("VerticalP") !=0.0f)))
                {
                    source.PlayOneShot(audioManager.dash);
                    StartCoroutine(DashMove());
                }

                    moveInput = new Vector3(Input.GetAxisRaw("HorizontalP"), 0f, Input.GetAxisRaw("VerticalP")).normalized;

                if ((Vector3.right * Input.GetAxisRaw("HorizontalR") + Vector3.forward * Input.GetAxisRaw("VerticalR")).sqrMagnitude <= 0.0f && moveInput != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(moveInput, Vector3.up);
                }
            }


            else if (playerNumber == 2)
            {
                if ((Input.GetKeyDown("joystick 2 button 5") && canDash) && ((Input.GetAxisRaw("HorizontalP 2") != 0.0f || Input.GetAxisRaw("VerticalP 2") != 0.0f)))
                {
                        source.PlayOneShot(audioManager.dash);
                        StartCoroutine(DashMove());
                }

                    moveInput = new Vector3(Input.GetAxisRaw("HorizontalP 2"), 0f, Input.GetAxisRaw("VerticalP 2")).normalized;

                if ((Vector3.right * Input.GetAxisRaw("HorizontalR 2") + Vector3.forward * Input.GetAxisRaw("VerticalR 2")).sqrMagnitude <= 0.0f && moveInput != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(moveInput, Vector3.up);
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
        if (isBuilding && canBuild)
        {
            moveVelocity = Vector3.zero;
        }

            if (moveVelocity == Vector3.zero && !isBuilding && !canBuild)
            {
                anim.SetInteger("condition", 0);
            }
            else if (moveVelocity != Vector3.zero && !isBuilding && !canBuild)
            {
                anim.SetInteger("condition", 1);
            }

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
            //transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            gun.isFiring = false;
            }
            #endregion
        }
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
            dashCoolDownTimer.SetDuration(dashCoolDown, 1);
            yield return null;
        }
        while(!dashCoolDownTimer.Update())
        {
            dashImage.fillAmount = dashCoolDownTimer.Progress();
            yield return null;
        }
        dashImage.fillAmount = 1f;
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

    public void Slow(float slowIntensity, float duration)
    {
        moveSpeed *= 0.5f;
        slowTimer.SetDuration(duration, 1);
        slowed = true;
    }

    public void Silence(float duration)
    {
        canSpell = false;
        silenceTimer.SetDuration(duration, 1);
        silenced = true;
    }

    public void SetSpeed(int _speed)
    {
        startingMoveSpeed = _speed;
        moveSpeed = _speed;
    }

    public void SetMaxHealth(int _maxHealth)
    {
        health.MaxHealth(_maxHealth);
    }

    public void ResetStats()
    {
        startingMoveSpeed = 15;
        moveSpeed = startingMoveSpeed;

        gun.ResetStats();
    }

}


