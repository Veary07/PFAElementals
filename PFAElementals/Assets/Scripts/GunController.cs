using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public bool isFiring = false;
    public bool damageBallTrigger = false;
    public BulletController bullet;

    [SerializeField] private float decal = 0.2f;


    public BulletController damageBall;
    public Timer damageBallTimer;
    private bool damageBallRestoring = false;
    [SerializeField] float ballSpeed;
    [SerializeField] float ballCoolDown;

    [SerializeField] float bulletSpeed;
    [SerializeField] float timeBetweenShots;

    [SerializeField] Transform firePosition;

    private float shotCounter;

    private Vector3 enemy = new Vector3(0,0,0);
    private bool monolithDestroyer = false;

    [SerializeField] int player = 1;

    private int target;

    private void Start()
    {
        if (player == 1)
            target = 10;
        else if (player == 2)
            target = 9;
    }


    // Update is called once per frame
    void Update ()
    {
        if (damageBallRestoring)
        {
            if (damageBallTimer.Update())
            {
                damageBallRestoring = false;
                damageBallTrigger = false;
            }
        }
        if (transform.localRotation.y < -60 || transform.localRotation.y > 60)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (damageBallTrigger && !damageBallRestoring)
        {
            BulletController newDamageBall = Instantiate(damageBall, firePosition.position, firePosition.rotation) as BulletController;
            newDamageBall.bulletSpeed = ballSpeed;
            newDamageBall.SetOwner(gameObject.GetComponent<GunController>());
            damageBallTimer.SetDuration(ballCoolDown, 1);
            damageBallRestoring = true;
        }

        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                BulletController newBullet = Instantiate(bullet, firePosition.position, new Quaternion(firePosition.rotation.x, firePosition.rotation.y + Random.Range(-decal, decal), firePosition.rotation.z, firePosition.rotation.w)) as BulletController;
                newBullet.bulletSpeed = bulletSpeed;
                newBullet.SetOwner(gameObject.GetComponent<GunController>());
                if (monolithDestroyer)
                    newBullet.SetMonolithDestroyerOn();
            }
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            shotCounter = 0;
        }

	}

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.layer == target) && (isFiring) && (other.gameObject.CompareTag("Player")))
        {
            if (-60f < transform.localRotation.eulerAngles.y  && transform.localRotation.eulerAngles.y  < 60f)
            {
                Vector3 direction = (other.transform.position - gameObject.transform.position).normalized;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.localRotation = Quaternion.identity;
    }

    public void SetMonolithDestroyerOn()
    {
        monolithDestroyer = true;
    }

    public void SetMonolithDestroyerOff()
    {
        monolithDestroyer = false;
    }
}
