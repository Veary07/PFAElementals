using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldSpell : MonoBehaviour
{

    public Timer shieldCoolDown;
    public Timer shieldDuration;
    private bool canSpell = true;
    private bool shielded = false;
    private bool restoring = false;

    private AudioManagerSO audioManager;
    AudioSource source;

    private HealthManager healthManager;

    [SerializeField] private int coolDown;
    [SerializeField] private int duration;
    [SerializeField] Image shieldImage;

    private void Start()
    {
        audioManager = Resources.FindObjectsOfTypeAll<AudioManagerSO>()[0];
        source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shielded == true)
        {
            source.PlayOneShot(audioManager.shield);
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
            shieldImage.fillAmount = shieldCoolDown.Progress();
            //shieldCoolDown.Update();
            if (shieldCoolDown.Update())
            {
                shieldImage.fillAmount = 1;
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
