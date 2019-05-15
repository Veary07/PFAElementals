using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolith : MonoBehaviour {

    [SerializeField] int Team = 1;
    public RespawnManager respawnManager;

    [SerializeField] Transform spawn;

    public HealthManager health;

    Monolith monolith;
	// Use this for initialization
	void Start ()
    {
        if (Team == 1)
            respawnManager.AddMonolithToListOne(monolith);

        if (Team == 2)
            respawnManager.AddMonolithToListTwo(monolith);
    }

    public Transform GetSpawner()
    {
        return spawn;
    }

    public void SetDamageableOn()
    {
        health.SetDamageableOn();
    }
    public void SetDamageableOff()
    {
        health.SetDamageableOff();
    }
}
