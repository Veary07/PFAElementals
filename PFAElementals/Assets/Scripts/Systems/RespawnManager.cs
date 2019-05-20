﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour {


    [SerializeField] Transform[] teamOneStartingSpawns;
    [SerializeField] Transform[] teamTwoStartingSpawns;
    [SerializeField] Monolith teamOneMonolith;
    [SerializeField] Monolith teamTwoMonolith;

    [SerializeField] List<Monolith> teamOneSpawn;
    [SerializeField] List<Monolith> teamTwoSpawn;

    Material map;
    [SerializeField] float interpolation = 2f;
    private bool isMoving = false;
    private float target;

    //public Monolith monolith;

    // Use this for initialization
    void Start ()
    {
        map = GameObject.Find("Ground").GetComponent<Renderer>().material;
        teamOneSpawn.Clear();
        teamTwoSpawn.Clear();
        for (int i = 0; i < teamOneStartingSpawns.Length; i++)
        {
            teamOneSpawn.Add(Instantiate(teamOneMonolith, teamOneStartingSpawns[i].transform.position, Quaternion.identity));
        }
        for (int i = 0; i < teamTwoStartingSpawns.Length; i++)
        {
            teamTwoSpawn.Add(Instantiate(teamTwoMonolith, teamTwoStartingSpawns[i].transform.position, Quaternion.identity));
        }

        for (int i = 0; i < teamOneSpawn.Count - 1; i++)
        {
            teamOneSpawn[i].GetComponent<Monolith>().SetDamageableOff();
        }

        for (int i = 0; i < teamTwoSpawn.Count - 1; i++)
        {
            teamTwoSpawn[i].GetComponent<Monolith>().SetDamageableOff();
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            map.SetFloat("Vector1_3ECABBA8", Mathf.Lerp(map.GetFloat("Vector1_3ECABBA8"), -target * 0.5f, Time.deltaTime * interpolation));
            if (map.GetFloat("Vector1_3ECABBA8") == -target)
            {
                isMoving = false;
            }
        }  
    }

    public void AddMonolithToListOne(Monolith Monolith)
    {
        teamOneSpawn.Add(Monolith);
    }
    public void AddMonolithToListTwo(Monolith Monolith)
    {
        teamTwoSpawn.Add(Monolith);
    }


    public Transform GetMonolith(int team)
    {
        if (team == 1)
        {
            if (teamOneSpawn.Count > 1)
            {
                return teamOneSpawn[teamOneSpawn.Count - 2].GetSpawner();
            }
            else
            {
                return teamOneSpawn[teamOneSpawn.Count - 1].GetSpawner();
            }

        }
        else if (team == 2)
        {
            if (teamTwoSpawn.Count > 1)
            {
                return teamTwoSpawn[teamTwoSpawn.Count - 2].GetSpawner();
            }
            else
            {
                return teamTwoSpawn[teamTwoSpawn.Count - 1].GetSpawner();
            }
        }
        else
        {
            return null;
        }
    }

    public void RemoveMonolith(int team)
    {
        if (team == 1)
        {
            teamOneSpawn.Remove(teamOneSpawn[teamOneSpawn.Count - 1]);
            teamOneSpawn[teamOneSpawn.Count - 1].GetComponent<Monolith>().SetDamageableOn();
        }

        if (team == 2)
        {
            teamTwoSpawn.Remove(teamTwoSpawn[teamTwoSpawn.Count - 1]);
            teamTwoSpawn[teamTwoSpawn.Count - 1].GetComponent<Monolith>().SetDamageableOn();
        }
    }

    public void AddMonolith(int team, Transform zone)
    {
        if (team == 1)
        {
            //map.SetFloat("Vector1_3ECABBA8", Mathf.Lerp(map.GetFloat("Vector1_3ECABBA8"), -zone.position.x * 0.5f, Time.deltaTime * interpolation));
            teamOneSpawn.Add(Instantiate(teamOneMonolith, zone.position, Quaternion.identity));
            teamOneSpawn[teamOneSpawn.Count - 2].GetComponent<Monolith>().SetDamageableOff();
        }

        if (team == 2)
        {
            //map.SetFloat("Vector1_3ECABBA8", Mathf.Lerp(map.GetFloat("Vector1_3ECABBA8"), - zone.position.x * 0.5f, Time.deltaTime * interpolation));
            teamTwoSpawn.Add(Instantiate(teamTwoMonolith, zone.position, Quaternion.identity));
            teamTwoSpawn[teamTwoSpawn.Count - 2].GetComponent<Monolith>().SetDamageableOff();
        }

        isMoving = true;
        target = zone.position.x;
    }


    public Monolith GetMonolithIndex(int team, int index)
    {
        if (team == 1 && teamOneSpawn[index]!= null)
        {
            return (teamOneSpawn[index]);
        }

        else if (team == 2 && teamTwoSpawn[index] != null)
        {
            return (teamTwoSpawn[index]);
        }

        else
        {
            return null;
        }
    }

    public int GetListCount (int team)
    {
        if (team == 1)
        {
            return teamOneSpawn.Count;
        }
        else if (team == 2)
        {
            return teamTwoSpawn.Count;
        }

        else
        {
            return 0;
        }
    }
}
