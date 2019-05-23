using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    [SerializeField] int damage;

    [SerializeField] int player = 1;
    [SerializeField] bool isABall = false;

    private int target;
    private bool monolithDestroyer;
    private GunController owner;
    [SerializeField] private float timer = 1f;
    private float startTimer;

    Timer accelerationTimer = new Timer();
    [SerializeField] float accelerationDuration;

    private bool bounce = false;
    

    [SerializeField] AnimationCurve accelerationCurve;

    private void Start()
    {
        accelerationTimer.SetDuration(accelerationDuration, 1, false);

        startTimer = Time.time;
        if (player == 1)
            target = 10;
        else if (player == 2)
            target = 9;
    }

    // Update is called once per frame
    void Update ()
    {
        float progress = 1f;
        if (isABall)
        {
            damage += 1;
        }

        if (Time.time > startTimer + timer)
        {
            Destroy(gameObject);
        }
        if (!accelerationTimer.Update())
        {
             progress = accelerationTimer.Progress();
        }   

        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime * accelerationCurve.Evaluate(progress));
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == target)
        {
            if (other.gameObject.tag == "Player" || (other.gameObject.tag == "Monolith" && !monolithDestroyer))
            {
            other.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
            other.gameObject.GetComponent<HealthManager>().SetKiller(owner);
            }
            else if (other.gameObject.tag == "Monolith" && monolithDestroyer)
            {
                other.gameObject.GetComponent<HealthManager>().SetKiller(owner);
                other.gameObject.GetComponent<HealthManager>().Termination();
            }

            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Interactive")
        {
            other.gameObject.GetComponent<Interactive>();
        }
        else if ((!other.gameObject.CompareTag("Player") && other.gameObject.layer != target) && (!other.gameObject.CompareTag("Bullet")))
        {
            if (bounce)
            {
                Vector3 v = Vector3.Reflect(transform.forward, other.contacts[0].normal);
                float rot = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
            }
            else
            {
                Destroy(gameObject);

            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }

    public void SetOwner(GunController Owner)
    {
        owner = Owner;
    }

    public void SetMonolithDestroyerOn()
    {
        monolithDestroyer = true;
    }
}
