using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] bool damageUp;
    [SerializeField] bool bouncer;
    [SerializeField] bool lifeSteal;
    [SerializeField] bool maxHealth;
    [SerializeField] bool speedUp;

    [SerializeField] int damage = 50;
    [SerializeField] int healthRecovery = 5;
    [SerializeField] int newMaxHealth = 150;
    [SerializeField] int speed = 25;

    public int index = -1;
    BonusSpawner bonusSpawner;

    GunController currentlyKillingMe = null;

    private void Start()
    {
        bonusSpawner = FindObjectOfType<BonusSpawner>();
    }

    public void DoStuff()
    {
        if (speedUp)
        {
            currentlyKillingMe.GetComponentInParent<PlayerController>().SetSpeed(speed);
        }

        if (maxHealth)
        {
            currentlyKillingMe.GetComponentInParent<HealthManager>().MaxHealth(newMaxHealth);

        }

        if (lifeSteal)
        {
            currentlyKillingMe.setLifeSteal(healthRecovery);
        }

        if (bouncer)
        {
            currentlyKillingMe.BounceOn();
        }

        if (damageUp)
        {
            currentlyKillingMe.DamageUp(damage);
        }

        bonusSpawner.AddNumberList(index);
        Destroy(gameObject);
    }

    public void SetKiller(GunController killer)
    {
        currentlyKillingMe = killer;
    }
}
