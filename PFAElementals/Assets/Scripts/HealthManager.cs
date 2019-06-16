using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    [SerializeField] int startMaxHealth;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] bool player = true;
    [SerializeField] int team = 1;
    [SerializeField] bool show = false;

    private int maxHealth;

    #region sounds
    public AudioClip spawn;
    private AudioManagerSO audioManager;
    AudioSource source;
    #endregion

    [SerializeField] HealthBar healthBar;

    public RespawnManager respawnManager;

    private PlayerController playerController;
    [SerializeField] private bool damageable = true;

    private int currentHealth = 100;
    private bool destroyed = false;
    private GunController currentlyKillingMe;

    public CameraShakerData data;

    public Animator anim;

    // Use this for initialization
    void Start ()
    {
        maxHealth = startMaxHealth;
        audioManager = Resources.Load("Sound Holder") as AudioManagerSO;
        source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
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
            //playerController.CanPlay = false;
            Kill();
        }
	}

    private void Kill()
    {
        if (player)
        {
            StartCoroutine(KillCoroutine());
        }


        else if (!player)
        {
            source.PlayOneShot(audioManager.totemDestruction);
            destroyed = true;
            respawnManager.RemoveMonolith(team);
            Destroy(transform.parent.gameObject);
        }

    }

    IEnumerator KillCoroutine()
    {
        ResetStats();
        //gameObject.SetActive(false);
        playerController.CanPlay = false;

        anim.SetInteger("condition", 0);
        yield return null;
        currentHealth = maxHealth;
        transform.position = respawnManager.GetMonolith(playerController.TeamNumber()).position;
        anim.SetInteger("condition", 3);
        source.PlayOneShot(spawn, 1f);

        currentlyKillingMe.SetMonolithDestroyerOn();
    }

    public void TakeDamage(int damage)
    {
        if (damageable)
        {
            Camera.main.GetComponentInParent<ShakeTransform>().AddShakeEvent(data);
            currentHealth -= damage;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

    }
    public void Termination()
    {
        if (damageable)
        {
            Camera.main.GetComponentInParent<ShakeTransform>().AddShakeEvent(data);
            currentHealth = 0;
        currentlyKillingMe.SetMonolithDestroyerOff();
        }

    }

    public int PlayerHealth()
    {
        return currentHealth;
    }

    public void SetKiller(GunController killer)
    {
        currentlyKillingMe = killer;
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

    public void MaxHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
        currentHealth = maxHealth;
    }

    public void ResetStats()
    {
        playerController.ResetStats();
        maxHealth = startMaxHealth;

    }
}
