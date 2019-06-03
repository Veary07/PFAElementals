using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    [SerializeField] int maxHealth;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] bool player = true;
    [SerializeField] int team = 1;
    [SerializeField] bool show = false;

    [SerializeField] HealthBar healthBar;

    public RespawnManager respawnManager;

    private PlayerController playerController;
    [SerializeField] private bool damageable = true;

    private int currentHealth = 100;
    private bool destroyed = false;
    private GunController currentlyKillingMe;

    public CameraShakerData data;

    Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        playerController = gameObject.GetComponent<PlayerController>();
        respawnManager = FindObjectOfType<RespawnManager>();
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float health = (float)currentHealth / (float)maxHealth;
        healthBar.SetSize(health);

        if (currentHealth <= 0 && destroyed == false)
        {
            Kill();
            anim.SetInteger("condition", 3);
            Debug.Log("Allo");
        }

        if (show)
        {
            Debug.Log(damageable);
        }
	}

    private void Kill()
    {
        if (player)
        {
<<<<<<< HEAD
            Debug.Log("Kill");
            //gameObject.SetActive(false);
=======
>>>>>>> 41d9d2e98c8c8a17720b181d1b131cdda58c5383
            
            currentHealth = maxHealth;
            transform.position = respawnManager.GetMonolith(playerController.TeamNumber()).position;
            currentlyKillingMe.SetMonolithDestroyerOn();

        }



        else if (!player)
        {
            destroyed = true;
            respawnManager.RemoveMonolith(team);
            Destroy(transform.parent.gameObject);
        }

    }

    public void TakeDamage(int damage)
    {
        if (damageable)
        {
            Camera.main.GetComponentInParent<ShakeTransform>().AddShakeEvent(data);
            currentHealth -= damage;
        }

    }
    public void Termination()
    {
        if (damageable)
        {
        currentHealth = 0;
        currentlyKillingMe.SetMonolithDestroyerOff();
        }

    }

    public int PlayerHealth()
    {
        return currentHealth;
    }

    public void SetKiller(GunController Killer)
    {
        currentlyKillingMe = Killer;
    }

    public void SetDamageableOn()
    {
        damageable = true;
    }
    public void SetDamageableOff()
    {
        damageable = false;
    }

    public float HealthPercentage()
    {
        return currentHealth * (maxHealth^-1);
    }
}
