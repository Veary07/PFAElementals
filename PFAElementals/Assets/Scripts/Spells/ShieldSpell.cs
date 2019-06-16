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
    [SerializeField] private float duration;
    [SerializeField] Image shieldImage;
    [SerializeField] GameObject shield;
    private GameObject shieldInstantiated = null;

    private void Start()
    {
        audioManager = Resources.Load("Sound Holder") as AudioManagerSO;
        source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shielded == true)
        {
            //shieldDuration.Update();
            if (shieldDuration.Update())
            {
                shield.transform.localScale = Vector3.Lerp(new Vector3(8, 8, 8), Vector3.zero, 1f);
                shieldCoolDown.SetDuration(coolDown, 1);
                restoring = true;
                shielded = false;
                healthManager.SetDamageableOn();
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
            shield.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(8, 8, 8), 1f);
            //shieldInstantiated = Instantiate(shield, transform.position + new Vector3(0,2,0), Quaternion.identity);
            //shieldInstantiated.transform.parent = this.gameObject.transform;
            source.PlayOneShot(audioManager.shield);
            canSpell = false;
            shielded = true;
            health.SetDamageableOff();
            shieldDuration.SetDuration(duration, 1);
        }
    }
}
