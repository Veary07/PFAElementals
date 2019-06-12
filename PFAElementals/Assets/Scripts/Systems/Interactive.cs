using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [SerializeField] bool zone = false;
    [SerializeField] bool explosive = false;
    [SerializeField] bool damager = false;
    [SerializeField] bool slower = false;
    [SerializeField] bool tree = false;
    [SerializeField] bool silencer = false;

    [SerializeField] int damage = 10;
    [SerializeField] float slow = 10f;
    [SerializeField] float duration = 2f;
    [SerializeField] float explosionForce = 10f;
    [SerializeField] float radius = 2f;

    public void DoStuff()
    {
        if (zone)
        {
            Collider[] colliders =  Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearbyObject in colliders)
            {
                if (slower)
                {
                    PlayerController playerController = nearbyObject.GetComponent<PlayerController>();
                    playerController.Slow(slow, duration);
                }

                if (damager)
                {
                    HealthManager health = nearbyObject.GetComponent<HealthManager>();
                    health.TakeDamage(damage);
                }

                if (explosive)
                {
                    Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(explosionForce, transform.position, radius);
                }

                if (silencer)
                {
                    PlayerController playerController = nearbyObject.GetComponent<PlayerController>();
                    playerController.Slow(slow, duration);
                }

            }
        }
    }
}
