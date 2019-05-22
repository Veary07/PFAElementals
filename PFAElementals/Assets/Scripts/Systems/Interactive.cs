using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [SerializeField] bool explosive = false;
    [SerializeField] bool damageable = false;
    [SerializeField] bool slowing = false;

    [SerializeField] float radiusExplosion = 0f;
    [SerializeField] int damage = 0;

    [SerializeField] float slowDuration;
    [SerializeField] float slowingSpeed;

    public void DoStuff()
    {
        if (explosive)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radiusExplosion);
            foreach (Collider nearbyObject in colliders)
            {
                if (nearbyObject.gameObject.tag == "Player")
                {
                    if (damageable)
                    {
                        nearbyObject.GetComponent<HealthManager>().TakeDamage(damage);
                    }
                    if (slowing)
                    {
                        nearbyObject.GetComponent<PlayerController>().Slow(slowDuration, slowingSpeed);
                    }
                }
            }
        }

    }
}
