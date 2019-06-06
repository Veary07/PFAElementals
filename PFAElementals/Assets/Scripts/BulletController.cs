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

    private AudioSource source;
    private AudioManagerSO audioManager;


    [SerializeField] AnimationCurve accelerationCurve;

    private void Start()
    {
        accelerationTimer.SetDuration(accelerationDuration, 1, false);
        audioManager = Resources.Load("Sound Holder") as AudioManagerSO;
        source = GameObject.Find("AudioManager").GetComponent<AudioSource>();


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
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime * accelerationCurve.Evaluate(progress));

        if (Time.time > startTimer + timer)
        {
            Destroy(gameObject);
        }
        if (!accelerationTimer.Update())
        {
            progress = accelerationTimer.Progress();
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (!isABall)
        {
            if (Physics.Raycast(ray, out hit, Time.deltaTime * bulletSpeed * accelerationCurve.Evaluate(progress)))
            {
                if (hit.transform.CompareTag("Player") || (hit.transform.tag == "Monolith" && !monolithDestroyer))
                {
                    source.PlayOneShot(audioManager.attackHit, 1f);
                    hit.transform.GetComponent<HealthManager>().TakeDamage(damage);
                    hit.transform.GetComponent<HealthManager>().SetKiller(owner);
                    Destroy(gameObject);

                }
                else if (hit.transform.tag == "Monolith" && monolithDestroyer)
                {
                    source.PlayOneShot(audioManager.totemHit, 1f);
                    hit.transform.GetComponent<HealthManager>().SetKiller(owner);
                    hit.transform.GetComponent<HealthManager>().Termination();
                    Destroy(gameObject);

                }
                else if (hit.transform.tag == "Interactive")
                {
                    hit.transform.GetComponent<Interactive>();
                }
                else if (hit.transform.CompareTag("Wall"))
                {
                    if (bounce)
                    {
                        Vector3 v = Vector3.Reflect(ray.direction, hit.normal);
                        float rot = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
                        transform.eulerAngles = new Vector3(0, rot, 0);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(isABall)
        {
            if (other.gameObject.layer == target)
            {
                if (other.gameObject.tag == "Player" || (other.gameObject.tag == "Monolith" && !monolithDestroyer))
                {
                    source.PlayOneShot(audioManager.fireBallHit, 1f);

                    other.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
                    other.gameObject.GetComponent<HealthManager>().SetKiller(owner);
                    Destroy(gameObject);
                }
                else if (other.gameObject.tag == "Monolith" && monolithDestroyer)
                {
                    other.gameObject.GetComponent<HealthManager>().SetKiller(owner);
                    other.gameObject.GetComponent<HealthManager>().Termination();
                }

                //Destroy(gameObject);
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
                   // Destroy(gameObject);

                }
            }
        }
       
    }

    //private void OnTriggerExit(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Finish")
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        //Destroy(gameObject);
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
