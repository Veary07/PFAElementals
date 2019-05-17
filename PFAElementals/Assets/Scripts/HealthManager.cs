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

    private CameraShake shake;

	// Use this for initialization
	void Start ()
    {
        shake = FindObjectOfType<CameraShake>();
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
            //gameObject.SetActive(false);
            currentHealth = maxHealth;
            transform.position = respawnManager.GetMonolith(playerController.TeamNumber()).position;
            //gameObject.SetActive(true); //TODO le mettre au bon endroit
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
         StartCoroutine(shake.Shake(.5f, 3f));
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
