using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpell : MonoBehaviour
{

    public Timer shieldCoolDown;
    public Timer shieldDuration;
    private bool canSpell = true;
    private bool shielded = false;
    private bool restoring = false;

    private HealthManager healthManager;

    [SerializeField] private int coolDown;
    [SerializeField] private int duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shielded == true)
        {
            //shieldDuration.Update();
            if (shieldDuration.Update())
            {
                healthManager.SetDamageableOn();
                shieldCoolDown.SetDuration(coolDown, 1);
                restoring = true;
                shielded = false;
            }
        }
        else if (restoring)
        {
            //shieldCoolDown.Update();
            if (shieldCoolDown.Update())
            {
                canSpell = true;
                restoring = false;
            }
        }
    }

    public void CastShield(HealthManager health)
    {
        healthManager = health;
        if (canSpell)
        {
            canSpell = false;
            shielded = true;
            health.SetDamageableOff();
            shieldDuration.SetDuration(duration, 1);
        }
        else
        {
            Debug.Log("CD");
        }
    }
    
}
